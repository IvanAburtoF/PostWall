using Microsoft.AspNetCore.Identity;
using PostWall.WEB.Data;
using System.ComponentModel.DataAnnotations;
namespace PostWall.Data.Models;

public class Comment
{
    public int Id { get; set; }
    [StringLength(255)]
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public ICollection<Report>? Reports { get; set; }
    public bool IsHidden { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}