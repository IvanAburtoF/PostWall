using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;

namespace PostWall.API.Models.DTO.Media;

public class CreateMediaDTO
{
    [Required]
    public string Url { get; set; } = null!;
    [Required]
    [EnumDataType(typeof(MediaType))]
    public string Type { get; set; } = null!;
}