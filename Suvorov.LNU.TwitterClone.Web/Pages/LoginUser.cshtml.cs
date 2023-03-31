using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suvorov.LNU.TwitterClone.Models.Frontend;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class LoginUserModel : PageModel
    {
        [BindProperty]
        public new LoginUserRequest User { get; set; }

        private readonly Database.Services.UserService _userService;

        public LoginUserModel(Database.Services.UserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if email address and password mathes
            if (await _userService.EmailAndPasswordMatch(User.EmailAddress, User.Password))
            {
                HttpContext.Session.SetString("userEmailAddress", User.EmailAddress);
                return RedirectToPage("/Index");
            }

            else
            {
                ModelState.AddModelError(string.Empty, "Invalid email address or password.");
                return Page();
            }
        }
    }
}
