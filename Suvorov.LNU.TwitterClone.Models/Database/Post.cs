namespace Suvorov.LNU.TwitterClone.Models.Database
{
    public class Post : DbItem
    {
        public string TextContent { get; set; }

        public byte[]? ImageContent { get; set; }

        public DateTime? PostDate { get; set; }

        public virtual User User { get; set; }
    }
}
