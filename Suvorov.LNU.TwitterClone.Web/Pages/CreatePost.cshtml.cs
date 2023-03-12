using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suvorov.LNU.TwitterClone.Database.Interfaces;
using Suvorov.LNU.TwitterClone.Models.Database;
using Suvorov.LNU.TwitterClone.Models.Frontend;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class CreatePostModel : PageModel
    {
        [BindProperty]
        public CreatePostRequest Post { get; set; }

        private readonly IDbEntityService<Post> _postService;

        public CreatePostModel(IDbEntityService<Post> postService)
        {
            _postService = postService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid) 
            {
                return Page();
            }

            await _postService.Create(new Post()
            {
                TextContent = Post.TextContent,
                User = Post.User,
            });

            return new RedirectToPageResult("/Posts");
        }
    }
}
