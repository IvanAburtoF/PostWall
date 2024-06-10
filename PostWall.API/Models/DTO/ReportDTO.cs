namespace PostWall.API.Models.DTO;

public class ReportDTO
{
    public int Id { get; set; }
    public string Reason { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string UserId { get; set; } = null!;
    public ApplicationUserDTO ApplicationUser { get; set; } = null!;
    public int PostId { get; set; }
    public PostDTO Post { get; set; } = null!;
    public int CommentId { get; set; }
    public CommentDTO Comment { get; set; } = null!;
}