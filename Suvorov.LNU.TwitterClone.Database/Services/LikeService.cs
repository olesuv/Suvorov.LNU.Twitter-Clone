using Microsoft.EntityFrameworkCore;
using Suvorov.LNU.TwitterClone.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Suvorov.LNU.TwitterClone.Database.Services
{
    public class LikeService : DbEntityService<Like>
    {
        private readonly NetworkDbContext _dbContext;

        public LikeService(NetworkDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Like> GetLikeByUserAndPost(User currentUser, Post currentPost)
        {
            return await _dbContext.Set<Like>().SingleOrDefaultAsync(like => like.User == currentUser && like.Post == currentPost);
        }
    }
}
