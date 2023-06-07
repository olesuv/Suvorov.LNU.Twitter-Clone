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
        [BindProperty]
        public User VisitorUser { get; set; }

        [BindProperty]
        public User ProfileUser { get; set; }

        public IList<Post> ProfileUserPosts { get; set; }

        public int ProfileUserPostsCount { get; set; }

        public List<(string Name, int Count)> mostUsedTags { get; set; }


        private readonly Database.Services.UserService _userService;

        private readonly PostService _postService;


        private readonly PostTagCountService _postTagCountService;

        private readonly LikeService _likeService;

        private readonly FollowService _followService;

        private readonly FolloweeService _followeeService;

        public UserProfileModel(Database.Services.UserService userService, PostService postService, 
            PostTagCountService postTagCountService, LikeService likeService, FollowService followService,
            FolloweeService followeeService)
        {
            _userService = userService;
            _postService = postService;
            _postTagCountService = postTagCountService;
            _likeService = likeService;
            _followService = followService;
            _followeeService = followeeService;
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

        public async Task<IActionResult> OnPostFollowUserAsync(int userId)
        {
            await OnGetAsync(userId);

            await _followService.Follow(ProfileUser, VisitorUser);
            await _followeeService.Followee(VisitorUser, ProfileUser);

            return new RedirectToPageResult("/Home");
        }

        public async Task<IActionResult> OnPostUnFollowUserAsync(int userId)
        {
            await OnGetAsync(userId);

            await _followService.Unfollow(ProfileUser, VisitorUser);
            await _followeeService.Unfollowee(VisitorUser, ProfileUser);

            return new RedirectToPageResult("/Home");
        }
    }
}
