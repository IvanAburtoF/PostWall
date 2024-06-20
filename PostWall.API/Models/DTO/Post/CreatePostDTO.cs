using PostWall.API.Models.DTO.Media;
using PostWall.API.Models.DTO.Tag;
using System.ComponentModel.DataAnnotations;

namespace PostWall.API.Models.DTO.Post;

public class CreatePostDTO
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Title { get; set; } = null!;    
    public ICollection<CreateTagDTO>? Tags { get; set; }
    [Required]
    public CreateMediaDTO Media { get; set; } = null!;
}
