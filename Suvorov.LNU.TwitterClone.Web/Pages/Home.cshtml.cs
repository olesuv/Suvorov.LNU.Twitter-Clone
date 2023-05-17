using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Suvorov.LNU.TwitterClone.Algorithms;
using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Database;
using Suvorov.LNU.TwitterClone.Models.Frontend;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class HomeModel : PageModel
    {
        [BindProperty]
        public CreatePostRequest Post { get; set; }

        public User UserInfo { get; private set; }

        public IList<Post> Posts { get; set; }

        private readonly Database.Services.UserService _userService;

        private readonly PostService _postService;

        public HomeModel(Database.Services.UserService userService, PostService postService)
        {
            _userService = userService;
            _postService = postService;
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

        public async Task<IActionResult> OnPost()
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
    }
}
