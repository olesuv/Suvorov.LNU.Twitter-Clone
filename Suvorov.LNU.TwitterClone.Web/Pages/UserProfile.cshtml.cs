using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Suvorov.LNU.TwitterClone.Algorithms;
using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Configuration;
using Suvorov.LNU.TwitterClone.Models.Database;
using Suvorov.LNU.TwitterClone.Models.Frontend;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class UserProfileModel : PageModel
    {
        public User VisitorUser { get; set; }

        public User ProfileUser { get; set; }

        public IList<Post> ProfileUserPosts { get; set; }

        public int ProfileUserPostsCount { get; set; }

        public List<(string Name, int Count)> mostUsedTags { get; set; }

        private readonly IOptions<AppConfig> _configuration;

        private readonly Database.Services.UserService _userService;

        private readonly PostService _postService;

        private readonly PostTagService _postTagService;

        private readonly PostTagCountService _postTagCountService;

        private readonly LikeService _likeService;

        public UserProfileModel(Database.Services.UserService userService, PostService postService, PostTagService postTagService,
                PostTagCountService postTagCountService, LikeService likeService, IOptions<AppConfig> configuration)
        {
            _userService = userService;
            _postService = postService;
            _postTagService = postTagService;
            _postTagCountService = postTagCountService;
            _likeService = likeService;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync(int userId)
        {
            ProfileUser = await _userService.GetById(userId);

            if (ProfileUser == null)
            {
                return NotFound();
            }

            var visitorUserEmail = HttpContext.Session.GetString("userEmailAddress");

            if (!string.IsNullOrEmpty(visitorUserEmail))
            {
                VisitorUser = await _userService.GetByEmail(visitorUserEmail);
            }

            ProfileUserPosts = await _postService.GetAllUserPosts(ProfileUser);

            mostUsedTags = await _postTagCountService.GetMostUsedTags(10);

            return Page();
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
