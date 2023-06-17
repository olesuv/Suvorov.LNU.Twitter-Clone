using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Configuration;
using Suvorov.LNU.TwitterClone.Models.Database;
using Suvorov.LNU.TwitterClone.Models.Frontend;
using Suvorov.LNU.TwitterClone.OpenAI;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class PostAIModel : PageModel
    {
        [BindProperty]
        public CreatePostRequest? Post { get; set; }

        public User? UserInfo { get; set; }

        private readonly Database.Services.UserService _userService;

        private readonly PostService _postService;

        private readonly PostTagService _postTagService;

        private readonly PostTagCountService _postTagCountService;

        private readonly IOptions<AppConfig> _configuration;

        private readonly string OpenAI_API_KEY;

        public PostAIModel(Database.Services.UserService? userService, PostService postService, 
        PostTagCountService postTagCountService, IOptions<AppConfig> configuration, PostTagService postTagService)
        {
            _userService = userService;
            _postService = postService;
            _postTagService = postTagService;
            _postTagCountService = postTagCountService;

            _configuration = configuration;
            OpenAI_API_KEY = _configuration.Value.OpenAI_API_KEY;
        }

        public async Task OnGetAsync()
        {
            var userEmail = HttpContext.Session.GetString("userEmailAddress");

            if (!string.IsNullOrEmpty(userEmail))
            {
                UserInfo = await _userService.GetByEmail(userEmail);
            }
            else
            {
                RedirectToPage("RegisterUser");
            }
        }

        public async Task<IActionResult> OnPostUserTweetUisngOpenAIAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            var userEmail = HttpContext.Session.GetString("userEmailAddress");
            var user = await _userService.GetByEmail(userEmail);

            var tweetsGenerator = new TweetsGenerator(OpenAI_API_KEY);

            var newPost = new Post()
            {
                TextContent = Post?.TextContent,
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

            return Redirect($"/UserProfile/{user.Id}");
        }
    }
}
