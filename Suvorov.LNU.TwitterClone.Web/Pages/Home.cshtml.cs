using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Database;
using Suvorov.LNU.TwitterClone.Models.Frontend;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class HomeModel : PageModel
    {
        [BindProperty]
        public User UserInfo { get; private set; }

        [BindProperty]
        public Post Post { get; private set; }

        private readonly Database.Services.UserService _userService;

        private readonly Database.Services.PostService _postService;

        public HomeModel(Database.Services.UserService userService, Database.Services.PostService postService)
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
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("userEmailAddress");
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPost()
        {
            if (UserInfo == null)
            {
                await OnGetAsync();
            }

            Post = new Post
            {
                TextContent = Request.Form["Post.TextContent"],
                PostDate = DateTime.Now,
                User = UserInfo
            };

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _postService.Create(Post);

            return new RedirectToPageResult("/Home");
        }
    }
}
