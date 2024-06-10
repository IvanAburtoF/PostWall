namespace PostWall.API.Models.EF;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Post>? Posts { get; set; }
}