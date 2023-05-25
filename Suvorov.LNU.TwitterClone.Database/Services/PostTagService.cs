using Microsoft.EntityFrameworkCore;
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
    }
}
