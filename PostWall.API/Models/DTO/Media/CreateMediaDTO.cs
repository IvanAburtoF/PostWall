using PostWall.API.Models.EF;
using System.ComponentModel.DataAnnotations;

namespace PostWall.API.Models.DTO.Media;

public class CreateMediaDTO
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Url { get; set; } = null!;
    [Required]
    [StringEnumValidation(typeof(MediaType))]
    public string Type { get; set; } = null!;
}