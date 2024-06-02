namespace PostWall.Data.Models;

public class Media
{
    public int Id { get; set; }
    public string Url { get; set; } = null!;
    public MediaType Type { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
}