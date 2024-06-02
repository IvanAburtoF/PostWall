namespace PostWall.Data.Models;

public class Media
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
}