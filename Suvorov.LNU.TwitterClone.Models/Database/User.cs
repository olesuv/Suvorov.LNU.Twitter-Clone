namespace Suvorov.LNU.TwitterClone.Models.Database
{
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? EmailAddress { get; set; }

        public int PhoneNumber { get; set; }

        public string? Password { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
