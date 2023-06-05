using Microsoft.EntityFrameworkCore;
using Suvorov.LNU.TwitterClone.Models.Database;

namespace Suvorov.LNU.TwitterClone.Database.Services
{
    public class PostService : DbEntityService<Post>
    {
        private readonly NetworkDbContext _dbContext;

        public PostService(NetworkDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Post>> SortAllPostsByPostDate()
        {
            return await _dbContext.Post.OrderByDescending(post => post.PostDate).ToListAsync();
        }

        public async Task<List<Post>> GetAllUserPosts(User user)
        {
            return await _dbContext.Post.Where(post => post.User == user).ToListAsync();
        }

        public async Task<int> CountAllUserPosts(User user)
        {
            List<Post> allUserPosts = await GetAllUserPosts(user);
            return allUserPosts.Count();
        }

        public async Task<List<Post>> SortUserPostsByPostDate(User user)
        {
            List<Post> allUserPostsSortedByDate = await GetAllUserPosts(user);
            return (List<Post>)allUserPostsSortedByDate.OrderByDescending(post => post.PostDate);
        }
    }
}
