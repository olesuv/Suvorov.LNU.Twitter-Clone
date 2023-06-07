namespace Suvorov.LNU.TwitterClone.Models.Database
{
    public class Followee : DbItem
    {
        public virtual User? CurrentUser { get; set; }

        public virtual ICollection<User>? UsersWhomFollowing { get; set; }

        public int? FollowingAmount { get; set; }
    }
}
