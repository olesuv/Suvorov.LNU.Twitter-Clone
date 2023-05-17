using Microsoft.EntityFrameworkCore;
using Suvorov.LNU.TwitterClone.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Suvorov.LNU.TwitterClone.Database.Services
{
    public class PostService : DbEntityService<Post>
    {
        private readonly NetworkDbContext _dbContext;

        public PostService(NetworkDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Post>> SortByPostDate()
        {
            return await _dbContext.Post.OrderByDescending(post => post.PostDate).ToListAsync();
        }
    }
}
