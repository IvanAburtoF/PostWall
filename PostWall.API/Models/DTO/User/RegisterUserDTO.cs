using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PostWall.API.Models.DTO.User;

public class RegisterUserDTO
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string UserName { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    [StringLength(100, MinimumLength = 6)]
    [PasswordPropertyText(true)]
    public string Password { get; set; } = null!;
}
