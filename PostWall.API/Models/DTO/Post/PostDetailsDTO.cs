using PostWall.API.Models.DTO.Comment;
using PostWall.API.Models.DTO.Media;
using PostWall.API.Models.DTO.Tag;
using PostWall.API.Models.DTO.User;

namespace PostWall.API.Models.DTO.Post;

public class PostDetailsDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public ICollection<TagDTO>? Tags { get; set; }
    public ICollection<CommentDetailsDTO>? Comments { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public MediaDTO Media { get; set; } = null!;
    public bool IsHidden { get; set; } = false;
    public UserDetailsDTO ApplicationUser { get; set; } = null!;
}
