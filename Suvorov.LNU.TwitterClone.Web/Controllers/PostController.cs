using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suvorov.LNU.TwitterClone.API.Controllers;
using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Database;
using System.Text.Json.Serialization;
using System.Text.Json;
using Suvorov.LNU.TwitterClone.Algorithms;
using Microsoft.Extensions.Hosting;

using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using UserService = Suvorov.LNU.TwitterClone.Database.Services.UserService;
using Microsoft.Extensions.Options;
using Suvorov.LNU.TwitterClone.Models.Configuration;
using Suvorov.LNU.TwitterClone.OpenAI;

namespace Suvorov.LNU.TwitterClone.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly PostService _postService;
        private readonly UserService _userService;
        private readonly PostTagService _postTagService;
        private readonly PostTagCountService _postTagCountService;
        private readonly IOptions<AppConfig> _configuration;
        private readonly string OpenAI_API_KEY;

        public PostController(ILogger<UserController> logger, PostService postService, 
               UserService userService, PostTagService postTagService, 
               PostTagCountService postTagCountService, IOptions<AppConfig> configuration)
        {
            _logger = logger;
            _postService = postService;
            _userService = userService;
            _postTagService = postTagService;
            _postTagCountService = postTagCountService;
            _configuration = configuration;

            OpenAI_API_KEY = _configuration.Value.OpenAI_API_KEY;
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Post>> GetById(int id)
        {
            try
            {
                var post = await _postService.GetById(id);
                if (post == null)
                {
                    return NotFound();
                }

                var result = new
                {
                    post.Id,
                    post.TextContent,
                    post.LikesAmount,
                    post.Likes.Count,
                    post.Tags,
                    UserId = post.User.Id,
                    post.PostDate,
                };

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    MaxDepth = 32
                };

                var serializedPost = JsonSerializer.Serialize(result, options);
                return Content(serializedPost, "application/json");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving post by Id");
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<ActionResult<Post>> CreatePost([FromForm] int userId, [FromForm] string postText)
        {
            try
            {
                var user = await _userService.GetById(userId);

                var newPost = new Post()
                {
                    TextContent = postText,
                    PostDate = DateTime.Now,
                    LikesAmount = 0,
                    User = user
                };

                await _postService.Create(newPost);

                List<string> tags = await GeneratedTweetsCorrection.ExtractTagsFromPost(postText);

                foreach (var tag in tags)
                {
                    var newPostTag = new PostTag()
                    {
                        Name = tag,
                        Post = newPost
                    };

                    await _postTagService.Create(newPostTag);
                    await _postTagCountService.IncrementTagCount(newPostTag);
                }

                var result = new
                {
                    newPost.Id,
                    newPost.PostDate,
                    newPost.TextContent,
                    newPost.LikesAmount,
                    UserId = newPost.User.Id,
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Post");
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("createWithAI")]
        [AllowAnonymous]
        public async Task<ActionResult<Post>> CreatePostUsingAI([FromForm] int userId, [FromForm] string postText)
        {
            try
            {
                var user = await _userService.GetById(userId);

                var tweetsGenerator = new TweetsGenerator(OpenAI_API_KEY);
                var userPrompt = postText;

                var newPost = new Post()
                {
                    TextContent = await tweetsGenerator.GenerateTweetText(userPrompt),
                    PostDate = DateTime.Now,
                    LikesAmount = 0,
                    User = user
                };

                await _postService.Create(newPost);

                var newPostAITag = new PostTag()
                {
                    Name = "generatedWithAI",
                    Post = newPost
                };

                await _postTagService.Create(newPostAITag);
                await _postTagCountService.IncrementTagCount(newPostAITag);

                List<string> generatedHashtags = await tweetsGenerator.GenerateTweetHashtags(newPost.TextContent);

                foreach (var hashtag in generatedHashtags)
                {
                    var newPostTag = new PostTag()
                    {
                        Name = hashtag,
                        Post = newPost
                    };

                    await _postTagService.Create(newPostTag);
                    await _postTagCountService.IncrementTagCount(newPostTag);
                }

                var result = new
                {
                    newPost.Id,
                    newPost.PostDate,
                    newPost.TextContent,
                    newPost.LikesAmount,
                    UserId = newPost.User.Id,
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Post");
                return StatusCode(500);
            }
        }
    }
}
