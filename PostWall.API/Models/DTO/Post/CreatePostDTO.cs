using PostWall.API.Models.DTO.Media;
using PostWall.API.Models.DTO.Tag;

namespace PostWall.API.Models.DTO.Post;

public class CreatePostDTO
{
    public string Title { get; set; } = null!;
    public ICollection<CreateTagDTO>? Tags { get; set; }
    public CreateMediaDTO Media { get; set; } = null!;
}
