using PostWall.API.Models.DTO.Media;
using PostWall.API.Models.DTO.Tag;
using System.ComponentModel.DataAnnotations;

namespace PostWall.API.Models.DTO.Post;

public class UpdatePostDTO
{
    [Required]
    public int? Id { get; set; }
    [StringLength(100, MinimumLength = 1)]
    public string Title { get; set; } = null!;
    public ICollection<TagDTO>? Tags { get; set; }
    //cant update media
    //public MediaDTO Media { get; set; } = null!;
    public bool? IsHidden { get; set; } = false;
}
