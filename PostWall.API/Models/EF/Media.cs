namespace PostWall.API.Models.EF;

public class Media
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public MediaType Type { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
}