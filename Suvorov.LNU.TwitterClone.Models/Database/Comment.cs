namespace Suvorov.LNU.TwitterClone.Models.Database
{
    public class Comment : DbItem
    {
        public string? CommentContent { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Post? Post { get; set; }

        public virtual User? User { get; set; }
    }
}
