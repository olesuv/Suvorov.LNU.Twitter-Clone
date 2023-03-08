using Suvorov.LNU.TwitterClone.Models.Database;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Suvorov.LNU.TwitterClone.Models.Frontend
{
    public class CreateUserRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }

        [Required]
        public int Age { get; set; }

        [AllowNull]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
    }
}
