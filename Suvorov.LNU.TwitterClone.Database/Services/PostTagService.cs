﻿using Microsoft.EntityFrameworkCore;
using Suvorov.LNU.TwitterClone.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Suvorov.LNU.TwitterClone.Database.Services
{
    public class PostTagService : DbEntityService<PostTag>
    {
        private readonly NetworkDbContext _dbContext;

        public PostTagService(NetworkDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PostTag>> GetMostUsedHashtags(int count)
        {
            var hashtagCounts = await _dbContext.PostTag
                .Where(pt => pt.Name != null)
                .GroupBy(pt => pt.Name)
                .OrderByDescending(group => group.Count())
                .Select(group => new PostTag { Id = 0, Name = group.Key }) // Assuming Id property exists
                .Take(count)
                .ToListAsync();

            return hashtagCounts;
        }
    }
}
