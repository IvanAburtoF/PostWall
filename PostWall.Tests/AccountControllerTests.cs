
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PostWall.API.Controllers;
using PostWall.API.Models.DTO.User;
using PostWall.API.Models.EF;

namespace PostWall.Tests;

public class AccountControllerTests
{    
    private readonly Mock<SignInManager<ApplicationUser>> _signInManager;
    private readonly Mock<UserManager<ApplicationUser>> _userManager;
    private AccountController _accountController;


    public AccountControllerTests()
    { 
        _signInManager = new Mock<SignInManager<ApplicationUser>>();
        _userManager = new Mock<UserManager<ApplicationUser>>();
        _accountController = new AccountController(_userManager.Object, _signInManager.Object);
    }

    [Fact]
    public async Task Register_WhenCalled_ReturnsOk()
    {
        // Arrange
        var model = new RegisterUserDTO
        {
            UserName = "test",
            Email = "emai@email.com",
            Password = "pasword"
        };
        _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _accountController.Register(model);
        Assert.NotNull(result);
        var okResult = result as OkResult;
        Assert.NotNull(okResult);
    }
}
