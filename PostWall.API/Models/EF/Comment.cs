using System.ComponentModel.DataAnnotations;

namespace PostWall.API.Models.EF;

public class Comment
{
    public int Id { get; set; }
    [StringLength(255)]
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Likes { get; set; } = 0;
    public int Dislikes { get; set; } = 0;
    public ICollection<Report> Reports { get; set; } = [];
    public bool IsHidden { get; set; } = false;

    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser ApplicationUser { get; set; } = null!;

    public ICollection<ApplicationUser>? LikedBy { get; set; } = [];
    public ICollection<ApplicationUser>? DislikedBy { get; set; } = [];
}