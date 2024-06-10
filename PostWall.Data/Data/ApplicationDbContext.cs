using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PostWall.Data.Models;

namespace PostWall.WEB.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Report> Reports { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<Media> Media { get; set; } = null!;
    }
}
