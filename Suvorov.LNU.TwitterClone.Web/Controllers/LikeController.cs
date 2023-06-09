using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suvorov.LNU.TwitterClone.API.Controllers;
using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Database;
using System.Text.Json.Serialization;
using System.Text.Json;
using Suvorov.LNU.TwitterClone.Web.Pages;

using UserService = Suvorov.LNU.TwitterClone.Database.Services.UserService;

namespace Suvorov.LNU.TwitterClone.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly LikeService _likeService;
        private readonly PostService _postService;
        private readonly UserService _userService;

        public LikeController(IMapper mapper, ILogger<UserController> logger, LikeService likeService,
                              PostService postService, UserService userService)
        {
            _mapper = mapper;
            _logger = logger;
            _likeService = likeService;
            _postService = postService;
            _userService = userService;
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> GetById(int id)
        {
            try
            {
                var like = await _likeService.GetById(id);
                if (like == null)
                {
                    return NotFound();
                }

                var result = new
                {
                    User = new
                    {
                        like.User.Id,
                        like.User.UserName,
                    },
                    Post = new
                    {
                        like.Post.Id,
                        like.Post.TextContent,
                    }
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user by Id");
                return StatusCode(500);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Like>> CreateLike([FromForm] int userId, [FromForm] int postId)
        {
            try
            {
                var currentPost = await _postService.GetById(postId);
                var currentUser = await _userService.GetById(userId);

                if (currentPost == null || currentUser == null)
                {
                    return BadRequest("Invalid post or user.");
                }

                var newLike = new Like()
                {
                    Post = currentPost,
                    User = currentUser
                };

                await _likeService.Create(newLike);

                currentPost.LikesAmount++;

                currentPost.Likes ??= new List<Like>();
                currentPost.Likes.Add(newLike);

                currentUser.LikedPosts ??= new List<Like>();
                currentUser.LikedPosts.Add(newLike);

                await _postService.Update(currentPost);
                await _userService.Update(currentUser);

                var result = new
                {
                    LikeId = newLike.Id,
                    UserId = userId,
                    PostId = postId,
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating like");
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var like = await _likeService.GetById(id);
                if (like == null)
                {
                    return NotFound();
                }

                var currentPost = await _postService.GetById(like.Post.Id);
                var currentUser = await _userService.GetById(like.User.Id);

                if (currentPost != null && currentUser != null)
                {
                    await _likeService.Delete(like);

                    currentPost.LikesAmount--;
                    currentUser.LikedPosts?.Remove(like);

                    await _postService.Update(currentPost);
                    await _userService.Update(currentUser);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting like");
                return StatusCode(500);
            }
        }
    }
}
