using System.ComponentModel.DataAnnotations;

namespace PostWall.API.Models.DTO.Comment;

public class CreateCommentDTO
{
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Content { get; set; } = null!;
    [Required]
    public int PostId { get; set; }
}