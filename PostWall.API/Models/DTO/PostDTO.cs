namespace PostWall.API.Models.DTO;

public class PostDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public ICollection<TagDTO>? Tags { get; set; }
    public ICollection<CommentDTO>? Comments { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public MediaDTO Media { get; set; } = null!;
    public ICollection<ReportDTO>? Reports { get; set; }
    public bool IsHidden { get; set; } = false;
    public string UserId { get; set; } = null!;
    public ApplicationUserDTO ApplicationUser { get; set; } = null!;
    public ICollection<ApplicationUserDTO>? LikedBy { get; set; }
    public ICollection<ApplicationUserDTO>? DislikedBy { get; set; }
}
