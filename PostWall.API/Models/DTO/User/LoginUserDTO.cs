using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PostWall.API.Models.DTO.User;

public class LoginUserDTO
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    [PasswordPropertyText(true)]
    public string Password { get; set; } = null!;
}
