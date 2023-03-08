using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suvorov.LNU.TwitterClone.Database.Interfaces;
using Suvorov.LNU.TwitterClone.Models.Database;
using Suvorov.LNU.TwitterClone.Models.Frontend;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class CreateUserModel : PageModel
    {
        [BindProperty]
        public CreateUserRequest User { get; set; }

        private readonly IDbEntityService<User> _userService;

        public CreateUserModel(IDbEntityService<User> userService)
        {
            _userService = userService;
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

            await _userService.Create(new User()
            {
                Name = User.Name,
                UserName = User.UserName,
                EmailAddress = User.EmailAddress,
                Password = User.Password,
                Age = User.Age,
                PhoneNumber = User?.PhoneNumber,
            });

            return new RedirectToPageResult("/Users");
        }
    }
}
