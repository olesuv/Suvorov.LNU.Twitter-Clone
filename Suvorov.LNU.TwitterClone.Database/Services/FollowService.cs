using Microsoft.EntityFrameworkCore;
using Suvorov.LNU.TwitterClone.Models.Database;

namespace Suvorov.LNU.TwitterClone.Database.Services
{
    public class FollowService : DbEntityService<Follow>
    {
        private readonly NetworkDbContext _dbContext;

        public FollowService(NetworkDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckIfFollowExists(User currentUser)
        {
            bool followExists = await _dbContext.Follow.AnyAsync(f => f.CuurentUser == currentUser);
            return followExists;
        }

        public async Task<bool> CheckIfFollowerExists(User currentUser, User whoFollowsUser)
        {
            bool followerExists = await _dbContext.Follow.AnyAsync(f => f.CuurentUser == currentUser && f.UsersWhoFollows.Contains(whoFollowsUser));
            return followerExists;
        }

        public async Task<Follow> GetByUser(User currentUser)
        {
            Follow? follow = await _dbContext.Follow.FirstOrDefaultAsync(f => f.CuurentUser == currentUser);
            return follow;
        }

        public async Task Follow(User currentUser, User whoFollowsUser)
        {
            bool followerExists = await CheckIfFollowerExists(currentUser, whoFollowsUser);

            if (!followerExists)
            {
                if (!await CheckIfFollowExists(currentUser))
                {
                    var newFollow = new Follow()
                    { 
                        CuurentUser = currentUser,
                        UsersWhoFollows = new List<User>(),
                        FollowersAmount = 0
                    };

                    await Create(newFollow);
                }

                Follow? currrentFollow = await GetByUser(currentUser);

                currrentFollow.UsersWhoFollows?.Add(whoFollowsUser);
                currrentFollow.FollowersAmount++;

                await SaveChanges();
            }
        }

        public async Task Unfollow(User currentUser, User whoFollowsUser)
        {
            bool followerExists = await CheckIfFollowerExists(currentUser, whoFollowsUser);

            if (followerExists)
            {
                if (await CheckIfFollowExists(currentUser))
                {
                    Follow? currrentFollow = await GetByUser(currentUser);

                    currrentFollow.UsersWhoFollows?.Remove(whoFollowsUser);
                    currrentFollow.FollowersAmount--;

                    await SaveChanges();
                }
            }
        }

        public async Task<int> GetFollowersAmount(User currentUser)
        {
            Follow follow = await _dbContext.Follow.FirstOrDefaultAsync(f => f.CuurentUser == currentUser);
            int followersAmount = follow?.FollowersAmount ?? 0;
            return followersAmount;
        }
    }
}
