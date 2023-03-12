using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suvorov.LNU.TwitterClone.Database.Interfaces;
using Suvorov.LNU.TwitterClone.Models.Database;
using Suvorov.LNU.TwitterClone.Models.Frontend;
using System.Globalization;

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

        public List<string> Months { get; set; }

        public List<int> Days { get; set; }

        public List<int> Years { get; set; }

        public void OnGet()
        {
            Months = Enumerable.Range(1, 12).Select(m => new DateTime(200, m, 1)).Select(d => d.ToString("MMMM")).ToList();
            Days = Enumerable.Range(1, 31).ToList();
            Years = Enumerable.Range(DateTime.Now.Year - 100, 101).ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }

            int year = User.SelectedYear;
            int month = DateTime.ParseExact(User.SelectedMonth, "MMMM", CultureInfo.CurrentCulture).Month;
            int day = User.SelectedDay;
            
            await _userService.Create(new User()
            {
                Name = User.Name,
                UserName = User.UserName,
                EmailAddress = User.EmailAddress,
                Password = User.Password,
                Birthday = new DateTime(year, month, day),
            });

            return new RedirectToPageResult("/Users");
        }
    }
}
