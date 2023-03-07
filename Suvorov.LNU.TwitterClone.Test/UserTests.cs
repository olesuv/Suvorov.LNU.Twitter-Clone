using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Suvorov.LNU.TwitterClone.Database;
using Suvorov.LNU.TwitterClone.Database.Interfaces;
using Suvorov.LNU.TwitterClone.Models.Configuration;
using Suvorov.LNU.TwitterClone.Models.Database;

namespace Suvorov.LNU.TwitterClone.Test
{
    [TestClass]
    public class UserTests : TestBase
    {
        IDbEntityService<User> _userService;
        IOptions<AppConfig> _configuration;
        NetworkDbContext _dbContext;

        public UserTests()
        {
            _dbContext = ResolveService<NetworkDbContext>();
            _userService = ResolveService<IDbEntityService<User>>();
            _configuration = ResolveService<IOptions<AppConfig>>();
        }

        [TestMethod]
        public async Task Create()
        {
            var users = await _userService.Create(new User()
            {
                Name = "Test User 1",
                UserName = "test_username_0",
                EmailAddress = "test.user0@gmail.com",
                Password = "test_password",
                Age = "8",
                RegistrationDate = DateTime.Now.Date,
            });
        }
    }
}
