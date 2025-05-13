using Microsoft.EntityFrameworkCore;
using UrlShortener.Entities;
using UrlShortener.Services;

namespace UrlShortener
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions optioons)
            : base(optioons) 
        { 
        }

        public DbSet<ShortUrl> ShortUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortUrl> (builder =>
            {
                builder.Property(s => s.Code).HasMaxLength(UrlShortServices.NumberOfCharsInShortLink);
                builder.HasIndex(s => s.Code).IsUnique();
            });
        }
    }
}
