namespace PostWall.API.Models.DTO;

public class CommentDTO
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public ICollection<ReportDTO>? Reports { get; set; }
    public bool IsHidden { get; set; }

    public int PostId { get; set; }
    public PostDTO Post { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public ApplicationUserDTO ApplicationUser { get; set; } = null!;

    public ICollection<ApplicationUserDTO>? LikedBy { get; set; }
    public ICollection<ApplicationUserDTO>? DislikedBy { get; set; }
}