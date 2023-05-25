using Microsoft.EntityFrameworkCore;
using Suvorov.LNU.TwitterClone.Models.Database;
using System;
using System.Threading.Tasks;

namespace Suvorov.LNU.TwitterClone.Database.Services
{
    public class PostTagCountService : DbEntityService<PostTagCount>
    {
        private readonly NetworkDbContext _dbContext;

        public PostTagCountService(NetworkDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PostTagCount?> GetByTagName(string tag)
        {
            try
            {
                return await _dbContext.Set<PostTagCount>().SingleOrDefaultAsync(x => x.PostTag.Name == tag);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving the PostTagCount by tag name: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> PostTagExists(string tag)
        {
            try
            {
                return await _dbContext.Set<PostTagCount>().AnyAsync(x => x.PostTag.Name == tag);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking if the PostTag exists: {ex.Message}");
                return false;
            }
        }

        public async Task IncrementTagCount(PostTag tag)
        {
            try
            {
                if (await PostTagExists(tag.Name))
                {
                    var currentTagCount = await _dbContext.Set<PostTagCount>().SingleOrDefaultAsync(x => x.PostTag.Name == tag.Name);

                    if (currentTagCount != null)
                    {
                        currentTagCount.Count++;
                        await SaveChanges();
                    }
                }
                else
                {
                    var newPostTagCount = new PostTagCount()
                    {
                        PostTag = tag,
                        Count = 1
                    };

                    await Create(newPostTagCount);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while incrementing the tag count: {ex.Message}");
            }
        }

    public async Task<List<(string Name, int Count)>> GetMostUsedTags(int amount)
        {
            try
            {
                var mostUsedTags = await _dbContext.Set<PostTagCount>()
                    .OrderByDescending(x => x.Count)
                    .Take(amount)
                    .Select(x => new { TagName = x.PostTag.Name, x.Count })
                    .ToListAsync();

                return mostUsedTags.Select(x => (x.TagName, x.Count)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving the most used tags: {ex.Message}");
                return new List<(string Name, int Count)>();
            }
        }
    }
}
