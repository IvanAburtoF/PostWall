using Microsoft.AspNetCore.Identity;
namespace PostWall.Data.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public ICollection<Report> Reports { get; set; }
    public bool IsHidden { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}