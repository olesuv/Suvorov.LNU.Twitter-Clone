namespace Suvorov.LNU.TwitterClone.Models.Database
{
    public class Like : DbItem
    {
        public virtual Post? Post { get; set; }

        public virtual User? User { get; set; }
    }
}
