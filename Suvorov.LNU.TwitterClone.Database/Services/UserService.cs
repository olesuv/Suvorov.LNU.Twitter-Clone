using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Suvorov.LNU.TwitterClone.Database.Interfaces;
using Suvorov.LNU.TwitterClone.Models.Database;
using System.Text.RegularExpressions;
using BCrypt.Net;

namespace Suvorov.LNU.TwitterClone.Database.Services
{
    public class UserService : DbEntityService<User>
    {
        private readonly NetworkDbContext _dbContext;

        public UserService(NetworkDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> UserNameExists(string userName)
        {
            return await _dbContext.Set<User>().AnyAsync(x => x.UserName == userName);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _dbContext.Set<User>().AnyAsync(x => x.EmailAddress == email);
        }

        public async Task<bool> EmailAndPasswordMatch(string email, string password)
        {
            var user = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.EmailAddress == email);

            if (user == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.EmailAddress == email);
        }
    }
}
