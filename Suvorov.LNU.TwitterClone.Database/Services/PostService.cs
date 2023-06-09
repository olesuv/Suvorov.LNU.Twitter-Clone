using Microsoft.EntityFrameworkCore;
using Suvorov.LNU.TwitterClone.Models.Database;
using System.Reflection.Metadata.Ecma335;

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
            return allUserPostsSortedByDate.OrderByDescending(post => post.PostDate).ToList();
        }

        public async Task<List<Post>> SortForLast72h(User user)
        {
            DateTime cutoffDate = DateTime.UtcNow.AddHours(-72);

            List<Post> userLast72hPosts = await _dbContext.Post
                .Where(post => post.User.Id == user.Id && post.PostDate >= cutoffDate)
                .OrderByDescending(post => post.PostDate)
                .ToListAsync();

            return userLast72hPosts;
        }

        public async Task<List<Post>> GetMostLikedPostsForLast72h()
        {
            DateTime cutoffDate = DateTime.UtcNow.AddHours(-72);

            List<Post> mostLikedPosts = await _dbContext.Post
                .Where(post => post.PostDate >= cutoffDate)
                .OrderByDescending(post => post.LikesAmount)
                .ThenByDescending(post => post.PostDate)
                .ToListAsync();

            return mostLikedPosts;
        }
    }
}
