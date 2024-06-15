using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PostWall.API.Models.EF;

namespace PostWall.Data
{
    public class PostWallDbContext(DbContextOptions<PostWallDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Report> Reports { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<Media> Media { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(a => a.UserName)
                .IsUnique();
            modelBuilder.Entity<Post>()
                .HasOne(p => p.ApplicationUser)
                .WithMany(a => a.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Report>()
                .HasOne(r => r.ApplicationUser)
                .WithMany(a => a.Reports)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Posts);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.LikedPosts)
                .WithMany(p => p.LikedBy);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.DislikedPosts)
                .WithMany(p => p.DislikedBy);
            modelBuilder.Entity<Comment>()
                .HasMany(c => c.LikedBy)
                .WithMany(a => a.LikedComments);
            modelBuilder.Entity<Comment>()
                .HasMany(c => c.DislikedBy)
                .WithMany(a => a.DislikedComments);
        }
    }
}
