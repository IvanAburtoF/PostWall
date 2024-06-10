using PostWall.Data;

namespace PostWall.API.Models.EF;

public class Report
{
    public int Id { get; set; }
    public string Reason { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public bool IsReviewed { get; set; } = false;
    public int CommentId { get; set; }
    public Comment Comment { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
}