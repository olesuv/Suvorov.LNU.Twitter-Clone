using Microsoft.EntityFrameworkCore;
using Suvorov.LNU.TwitterClone.Models.Database;

namespace Suvorov.LNU.TwitterClone.Database.Services
{
    public class FolloweeService : DbEntityService<Followee>
    {
        private readonly NetworkDbContext _dbContext;

        public FolloweeService(NetworkDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckIfFollowExists(User currentUser)
        {
            bool followExists = await _dbContext.Followee.AnyAsync(f => f.CurrentUser == currentUser);
            return followExists;
        }

        public async Task<bool> CheckIfFollowerExists(User currentUser, User whoseReadingCurrentUser)
        {
            bool followerExists = await _dbContext.Followee.AnyAsync(f => f.CurrentUser == currentUser && f.UsersWhomFollowing.Contains(whoseReadingCurrentUser));
            return followerExists;
        }

        public async Task<Followee> GetByUser(User currentUser)
        {
            Followee? follow = await _dbContext.Followee.FirstOrDefaultAsync(f => f.CurrentUser == currentUser);
            return follow;
        }

        public async Task Followee(User currentUser, User whoseReadingCurrentUser)
        {
            bool followeeExists = await CheckIfFollowerExists(currentUser, whoseReadingCurrentUser);

            if (!followeeExists)
            {
                if (!await CheckIfFollowExists(currentUser))
                {
                    var newFollowee = new Followee()
                    {
                        CurrentUser = currentUser,
                        UsersWhomFollowing = new List<User>(),
                        FollowingAmount = 0
                    };

                    await Create(newFollowee);
                }

                Followee? currrentFollow = await GetByUser(currentUser);

                currrentFollow.UsersWhomFollowing?.Add(whoseReadingCurrentUser);
                currrentFollow.FollowingAmount++;

                await SaveChanges();
            }
        }

        public async Task Unfollowee(User currentUser, User whoseReadingCurrentUser)
        {
            bool followerExists = await CheckIfFollowerExists(currentUser, whoseReadingCurrentUser);

            if (followerExists)
            {
                if (await CheckIfFollowExists(currentUser))
                {
                    Followee? currentFollow = await GetByUser(currentUser);

                    currentFollow.UsersWhomFollowing?.Remove(whoseReadingCurrentUser);
                    currentFollow.FollowingAmount--;

                    await SaveChanges();
                }
            }
        }

        public async Task<int> GetFolloweeAmount(User currentUser)
        {
            Followee follow = await _dbContext.Followee.FirstOrDefaultAsync(f => f.CurrentUser == currentUser);
            int followingsAmount = follow?.FollowingAmount ?? 0;
            return followingsAmount;
        }
    }
}
