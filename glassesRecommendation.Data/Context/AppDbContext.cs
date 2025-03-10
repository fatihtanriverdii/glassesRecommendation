using glassesRecommendation.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace glassesRecommendation.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Glasses> Glasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(g => g.Glasses)
                .WithMany(u => u.Users);

            base.OnModelCreating(modelBuilder);
        }
    }
}
