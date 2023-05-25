using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Suvorov.LNU.TwitterClone.Database.Interfaces;
using Suvorov.LNU.TwitterClone.Models.Database;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class UsersModel : PageModel
    {
        public IList<User> Users { get; set; }

        private readonly IDbEntityService<User> _userService;

        public UsersModel(IDbEntityService<User> userService)
        {
            _userService = userService;
        }

        public async Task OnGet()
        {
            Users = await _userService.GetAll().ToListAsync();
        }
    }
}
