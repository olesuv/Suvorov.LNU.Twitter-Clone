using Suvorov.LNU.TwitterClone.Models.Database;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Suvorov.LNU.TwitterClone.Models.Frontend
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Who are you?")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter valid email address.")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Date)] 
        public DateTime Birthday { get; set;}

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$", 
        ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter and one number.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string SelectedMonth { get; set; }

        [Required]
        public int SelectedDay { get; set; }

        [Required]
        public int SelectedYear { get; set; }
    }
}
