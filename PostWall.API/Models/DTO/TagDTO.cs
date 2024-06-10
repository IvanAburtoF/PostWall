namespace PostWall.API.Models.DTO;

public class TagDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<PostDTO>? Posts { get; set; }
}