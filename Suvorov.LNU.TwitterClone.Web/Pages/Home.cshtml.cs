using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Suvorov.LNU.TwitterClone.Algorithms;
using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Configuration;
using Suvorov.LNU.TwitterClone.Models.Database;
using Suvorov.LNU.TwitterClone.Models.Frontend;
using Suvorov.LNU.TwitterClone.OpenAI;
using System.Text.RegularExpressions;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class HomeModel : PageModel
    {
        [BindProperty]
        public CreatePostRequest Post { get; set; }

        public User UserInfo { get; set; }

        public IList<Post> Posts { get; set; }

        public PostTag PostTag { get; set; }

        public List<(string Name, int Count)> mostUsedTags { get; set; }

        private readonly string OpenAI_API_KEY;

        private readonly IOptions<AppConfig> _configuration;

        private readonly Database.Services.UserService _userService;

        private readonly PostService _postService;

        private readonly PostTagService _postTagService;

        private readonly PostTagCountService _postTagCountService;

        private readonly LikeService _likeService;

        private readonly FolloweeService _followeeService;

        private readonly IPostRecommendations _postRecommendations;

        public HomeModel(Database.Services.UserService userService, PostService postService, PostTagService postTagService, 
                        PostTagCountService postTagCountService, LikeService likeService, FolloweeService followeeService,
                        IPostRecommendations postRecommendations, IOptions<AppConfig> configuration)
        {
            _userService = userService;
            _postService = postService;
            _postTagService = postTagService;
            _postTagCountService = postTagCountService;
            _likeService = likeService;
            _followeeService = followeeService;
            _postRecommendations = postRecommendations;
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

            Posts = await _postRecommendations.GeneratePostRecommendations(UserInfo);

            mostUsedTags = await _postTagCountService.GetMostUsedTags(5);
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

            var newPost = new Post()
            {
                TextContent = Post.TextContent,
                PostDate = DateTime.Now,
                LikesAmount = 0,
                User = user
            };

            await _postService.Create(newPost);

            List<string> tags = await GeneratedTweetsCorrection.ExtractTagsFromPost(Post.TextContent);

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

            return new RedirectToPageResult("/Home");
        }

        public async Task<IActionResult> OnPostUserLikePostAsync(int currentPostId)
        {
            var currentPost = await _postService.GetById(currentPostId);

            var userEmail = HttpContext.Session.GetString("userEmailAddress");
            var currentUser = await _userService.GetByEmail(userEmail);

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

            return new RedirectToPageResult("/Home");
        }

        public async Task<IActionResult> OnPostUserUnLikePostAsync(int currentPostId)
        {
            var currentPost = await _postService.GetById(currentPostId);

            var userEmail = HttpContext.Session.GetString("userEmailAddress");
            var currentUser = await _userService.GetByEmail(userEmail);

            var like = await _likeService.GetLikeByUserAndPost(currentUser, currentPost);

            if (like != null)
            {
                await _likeService.Delete(like);

                currentPost.LikesAmount--;

                currentUser?.LikedPosts?.Remove(like);

                await _postService.Update(currentPost);
            }

            return new RedirectToPageResult("/Home");
        }
    }
}
