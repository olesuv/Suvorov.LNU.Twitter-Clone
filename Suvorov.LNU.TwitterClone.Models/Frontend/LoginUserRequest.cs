using Suvorov.LNU.TwitterClone.Models.Database;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Suvorov.LNU.TwitterClone.Models.Frontend
{
    public class LoginUserRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter valid email address.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [EmailAddress(ErrorMessage = "Password is incorrect.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
