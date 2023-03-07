namespace Suvorov.LNU.TwitterClone.Models.Database
{
    public class User : DbItem
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string Age { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string? PhoneNumber { get; set; }

        public byte[]? ProfileImage { get; set; }
    }
}
