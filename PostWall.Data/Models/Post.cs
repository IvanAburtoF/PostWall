namespace PostWall.Data.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Tag> Tags { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public Media Media { get; set; }
    public ICollection<Report> Reports { get; set; }
    public bool IsHidden { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}
