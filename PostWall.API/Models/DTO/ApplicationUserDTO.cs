namespace PostWall.API.Models.DTO;

public class ApplicationUserDTO
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public ICollection<PostDTO>? Posts { get; set; }
    public ICollection<CommentDTO>? Comments { get; set; }
    public ICollection<ReportDTO>? Reports { get; set; }
    public ICollection<PostDTO>? LikedPosts { get; set; }
    public ICollection<PostDTO>? DislikedPosts { get; set; }
    public ICollection<CommentDTO>? LikedComments { get; set; }
    public ICollection<CommentDTO>? DislikedComments { get; set; }
}