using PostWall.API.Models.DTO.Comment;
using PostWall.API.Models.DTO.Post;
using PostWall.API.Models.DTO.User;

namespace PostWall.API.Models.DTO.Report;

public class ReportDetailsDTO
{
    public int? Id { get; set; }
    public string Reason { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public UserDetailsDTO ApplicationUser { get; set; } = null!;
    public PostDetailsDTO Post { get; set; } = null!;
    public CommentDetailsDTO Comment { get; set; } = null!;
}