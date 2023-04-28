using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Database;
using Suvorov.LNU.TwitterClone.Models.Frontend;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class PostService : PageModel
    {
        [BindProperty]
        public User UserInfo { get; set; }

        [BindProperty]
        public new CreatePostRequest Post { get; set; }

        private readonly Database.Services.UserService _userService;

        private readonly Database.Services.PostService _postService;

        public PostService(Database.Services.UserService userService, Database.Services.PostService postService)
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
            if (!ModelState.IsValid)
            {
                OnGetAsync();
                return Page();
            }

            var userEmail = HttpContext.Session.GetString("userEmailAddress");
            var user = await _userService.GetByEmail(userEmail);

            await _postService.Create(new Post()
            {
                TextContent = Post.TextContent,
                ImageContent = Post.ImageContent,
                PostDate = DateTime.Now,
                User = user,
            });

            return new RedirectToPageResult("/Home");
        }
    }
}
