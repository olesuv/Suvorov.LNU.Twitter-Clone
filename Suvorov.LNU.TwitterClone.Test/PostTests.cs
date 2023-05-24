using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Suvorov.LNU.TwitterClone.Database;
using Suvorov.LNU.TwitterClone.Database.Interfaces;
using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Configuration;
using Suvorov.LNU.TwitterClone.Models.Database;

namespace Suvorov.LNU.TwitterClone.Test
{
    [TestClass]
    public class PostTest : TestBase
    {
        UserService _userService;
        PostService _postService;
        IOptions<AppConfig> _configuration;
        NetworkDbContext _dbContext;

        public PostTest()
        {
            _dbContext = ResolveService<NetworkDbContext>();
            _userService = ResolveService<UserService>();
            _postService = ResolveService<PostService>();
            _configuration = ResolveService<IOptions<AppConfig>>();
        }
    }
}
