using Suvorov.LNU.TwitterClone.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suvorov.LNU.TwitterClone.Database.Services
{
    public class PostService : DbEntityService<Post>
    {
        public PostService(NetworkDbContext dbContext) : base(dbContext)
        {
        }

        // Implement any additional methods specific to the Post entity
        //public async Task<Post> 
    }
}
