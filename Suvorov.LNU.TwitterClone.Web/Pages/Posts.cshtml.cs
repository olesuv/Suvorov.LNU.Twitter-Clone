using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Suvorov.LNU.TwitterClone.Database.Interfaces;
using Suvorov.LNU.TwitterClone.Models.Database;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class PostsModel : PageModel
    {
        public IList<Post> Posts { get; set; }

        private readonly IDbEntityService<Post> _postService;

        public PostsModel(IDbEntityService<Post> postService)
        {
            _postService = postService;
        }
        public async Task OnGet()
        {
            Posts = await _postService.GetAll().ToListAsync();
        }
    }
}
