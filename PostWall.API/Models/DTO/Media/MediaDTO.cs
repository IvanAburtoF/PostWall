namespace PostWall.API.Models.DTO.Media;

public class MediaDTO
{
    public int? Id { get; set; }
    public string Url { get; set; } = null!;
    public string Type { get; set; } = null!;
}