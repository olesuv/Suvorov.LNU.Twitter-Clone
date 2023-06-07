namespace Suvorov.LNU.TwitterClone.Models.Database
{
    public class Follow : DbItem
    {
        public virtual User? CuurentUser { get; set; }

        public virtual ICollection<User>? UsersWhoFollows { get; set; }

        public int? FollowersAmount { get; set; }
    }
}
