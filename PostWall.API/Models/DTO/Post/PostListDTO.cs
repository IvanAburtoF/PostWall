using PostWall.API.Models.DTO.Media;
using PostWall.API.Models.DTO.Tag;
using PostWall.API.Models.DTO.User;

namespace PostWall.API.Models.DTO.Post;

public class PostListDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public ICollection<TagDTO>? Tags { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public MediaDTO Media { get; set; } = null!;
    public UserDetailsDTO ApplicationUser { get; set; } = null!;
}