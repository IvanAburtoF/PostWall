using PostWall.API.Models.DTO.Media;
using PostWall.API.Models.DTO.Tag;

namespace PostWall.API.Models.DTO.Post;

public class UpdatePostDTO
{
    public int? Id { get; set; }
    public string Title { get; set; } = null!;
    public ICollection<TagDTO>? Tags { get; set; }
    public MediaDTO Media { get; set; } = null!;
    public bool IsHidden { get; set; } = false;
}
