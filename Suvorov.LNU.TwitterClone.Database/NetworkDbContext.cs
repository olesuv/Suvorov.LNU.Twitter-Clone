using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Suvorov.LNU.TwitterClone.Models.Database;

namespace Suvorov.LNU.TwitterClone.Database
{
    public class NetworkDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Post> Post { get; set; }

        public DbSet<PostTag> PostTag { get; set; }

        public DbSet<PostTagCount> PostTagCount { get; set; }

        public DbSet<Follow> Follow { get; set; }

        public DbSet<Followee> Followee { get; set; }

        public NetworkDbContext() { }

        public NetworkDbContext(DbContextOptions<NetworkDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .Property(u => u.RegistrationDate)
            .HasColumnType("date")
            .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Post>()
            .Property(u => u.PostDate)
            .HasDefaultValueSql("GETDATE()");
        }
    }
}
