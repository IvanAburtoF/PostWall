using PostWall.API.Models.DTO.Comment;
using PostWall.API.Models.DTO.Post;

namespace PostWall.API.Models.DTO.User;

public class UserDetailsDTO
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
}