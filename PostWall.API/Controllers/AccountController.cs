using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostWall.API.Models.DTO.User;
using PostWall.API.Models.EF;

namespace PostWall.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
        };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent:true, lockoutOnFailure:false);

        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest("Invalid login attempt");
    }
}
