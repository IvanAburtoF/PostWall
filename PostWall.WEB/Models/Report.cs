namespace PostWall.Data.Models;

public class Report
{
    public int Id { get; set; }
    public string Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsReviewed { get; set; }
    public int CommentId { get; set; }
    public Comment Comment { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}