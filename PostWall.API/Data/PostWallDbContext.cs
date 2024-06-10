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
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.Posts)
                .WithOne(p => p.ApplicationUser)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.ApplicationUser)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.Reports)
                .WithOne(r => r.ApplicationUser)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.LikedPosts)
                .WithMany(p => p.LikedBy)
                .UsingEntity(j => j.ToTable("LikedPosts"));
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.DislikedPosts)
                .WithMany(p => p.DislikedBy)
                .UsingEntity(j => j.ToTable("DislikedPosts"));
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.LikedComments)
                .WithMany(c => c.LikedBy)
                .UsingEntity(j => j.ToTable("LikedComments"));
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.DislikedComments)
                .WithMany(c => c.DislikedBy)
                .UsingEntity(j => j.ToTable("DislikedComments"));
        }
    }
}
