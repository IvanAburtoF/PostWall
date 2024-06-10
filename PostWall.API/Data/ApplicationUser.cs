using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostWall.API.Models.EF;
using System.ComponentModel.DataAnnotations;

namespace PostWall.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Required]
    public override string? UserName { get; set; }
    public ICollection<Post>? Posts { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Report>? Reports { get; set; }
    public ICollection<Post>? LikedPosts { get; set; }
    public ICollection<Post>? DislikedPosts { get; set; }
    public ICollection<Comment>? LikedComments { get; set; }
    public ICollection<Comment>? DislikedComments { get; set; }
}
