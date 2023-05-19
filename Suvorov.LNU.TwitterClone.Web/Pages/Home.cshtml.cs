using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Suvorov.LNU.TwitterClone.Algorithms;
using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Configuration;
using Suvorov.LNU.TwitterClone.Models.Database;
using Suvorov.LNU.TwitterClone.Models.Frontend;
using Suvorov.LNU.TwitterClone.OpenAI;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class HomeModel : PageModel
    {
        [BindProperty]
        public CreatePostRequest Post { get; set; }

        public User UserInfo { get; set; }

        public IList<Post> Posts { get; set; }

        public PostTag PostTag { get; set; }

        private readonly string OpenAI_API_KEY;

        private readonly IOptions<AppConfig> _configuration;

        private readonly Database.Services.UserService _userService;

        private readonly PostService _postService;

        private readonly PostTagService _postTagService;

        public HomeModel(Database.Services.UserService userService, PostService postService, PostTagService postTagService, IOptions<AppConfig> configuration)
        {
            _userService = userService;
            _postService = postService;
            _postTagService = postTagService;
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

            var postRecomendations = new PostRecomendations(_postService);
            Posts = await postRecomendations.GeneratePostRecomendations();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("userEmailAddress");
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostUserTweet()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            var userEmail = HttpContext.Session.GetString("userEmailAddress");
            var user = await _userService.GetByEmail(userEmail);

            await _postService.Create(new Post()
            {
                TextContent = Post.TextContent,
                PostDate = DateTime.Now,
                LikesAmount = 0,
                User = user
            });

            return new RedirectToPageResult("/Home");
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
            var userPrompt = Post.TextContent;

            var newPost = new Post()
            {
                TextContent = await tweetsGenerator.GenerateTweetText(userPrompt),
                PostDate = DateTime.Now,
                LikesAmount = 0,
                User = user
            };

            await _postService.Create(newPost);

            List<string> generatedHashtags = await tweetsGenerator.GenerateTweetHashtags(newPost.TextContent);

            foreach(var hashtag in generatedHashtags)
            {
                var newPostTag = new PostTag()
                {
                    Name = hashtag,
                    Post = newPost
                };

                await _postTagService.Create(newPostTag);
            }

            return new RedirectToPageResult("/Home");
        }
    }
}
