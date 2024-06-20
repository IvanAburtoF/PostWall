using PostWall.API.Models.DTO.Post;
using System.ComponentModel.DataAnnotations;

namespace PostWall.API.Models.DTO.Tag;

public class CreateTagDTO
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = null!;
}