namespace Suvorov.LNU.TwitterClone.Models.Database
{
    public class PostTag : DbItem
    {
        public string? Name { get; set; }

        public virtual Post? Post { get; set; }
    }
}
