namespace Suvorov.LNU.TwitterClone.Models.Database
{
    public class PostTagCount : DbItem
    {
        public virtual PostTag? PostTag { get; set; }

        public int Count { get; set; }
    }
}
