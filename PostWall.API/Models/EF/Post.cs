using PostWall.Data;

namespace PostWall.API.Models.EF;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public Media Media { get; set; } = null!;
    public ICollection<Report>? Reports { get; set; }
    public bool IsHidden { get; set; } = false;
    //identity
    public string UserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public ICollection<ApplicationUser>? LikedBy { get; set; }
    public ICollection<ApplicationUser>? DislikedBy { get; set; }
}
