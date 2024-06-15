namespace PostWall.API.Models.EF;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public virtual ICollection<Tag> Tags { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public int Likes { get; set; } = 0;
    public int Dislikes { get; set; } = 0;
    public Media Media { get; set; } = null!;
    public ICollection<Report> Reports { get; set; } = [];
    public bool IsHidden { get; set; } = false;
    //identity
    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    public ICollection<ApplicationUser> LikedBy { get; set; } = [];
    public ICollection<ApplicationUser> DislikedBy { get; set; } = [];
}
