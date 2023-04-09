using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Suvorov.LNU.TwitterClone.Database.Interfaces;
using Suvorov.LNU.TwitterClone.Models.Database;
using System.Text.RegularExpressions;
using BCrypt.Net;

namespace Suvorov.LNU.TwitterClone.Database.Services
{
    public class UserService : IDbEntityService<User>
    {
        private readonly NetworkDbContext _dbContext;
        private bool _disposed;

        public UserService(NetworkDbContext dbContext)
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

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> Create(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var entityFromOb = await _dbContext.Set<User>().AddAsync(user);
            await SaveChanges();
            return entityFromOb.Entity;
        }

        public async Task Delete(User entity)
        {
            _dbContext.Set<User>().Remove(entity);
            await SaveChanges();
        }

        public async Task<User> GetById(int id)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.EmailAddress == email);
        }

        public async Task<User> Update(User entity)
        {
            var entityFromOb = _dbContext.Set<User>().Update(entity);
            await SaveChanges();

            return entityFromOb.Entity;
        }

        public IQueryable<User> GetAll()
        {
            return _dbContext.Set<User>();
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _dbContext.Dispose();
            _disposed = true;
        }
    }
}
