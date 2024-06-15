using PostWall.API.Models.DTO.Post;
using PostWall.API.Models.DTO.User;

namespace PostWall.API.Models.DTO.Comment;

public class CommentDetailsDTO
{
    public int? Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public bool IsHidden { get; set; }
    public UserDetailsDTO ApplicationUser { get; set; } = null!;
}