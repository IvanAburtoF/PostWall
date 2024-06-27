
using Microsoft.AspNetCore.Http;
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
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        _userManager = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

        var contextAccessorMock = new Mock<IHttpContextAccessor>();
        var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        _signInManager = new Mock<SignInManager<ApplicationUser>>(_userManager.Object, contextAccessorMock.Object, userPrincipalFactoryMock.Object, null, null, null, null);

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
        //Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Register_WhenCalled_ReturnsBadRequest()
    {
        //Arrange
        var model = new RegisterUserDTO
        {
            UserName = "test",
            Email = ""
        };
        _accountController.ModelState.AddModelError("Email", "The Email field is required");
        _accountController.ModelState.AddModelError("Password", "The Password field is required");
        //Act
        var result = await _accountController.Register(model);
        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(2, _accountController.ModelState.ErrorCount);
    }
    [Fact]
    public async Task Login_WhenCalled_ReturnsOk()
    {
        //Arrange
        var model = new LoginUserDTO
        {
            Email = "email@email.com",
            Password = "password"
        };
        _signInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

        //Act
        var result = await _accountController.Login(model);
        //Assert
        Assert.IsType<OkResult>(result);
    }
    [Fact]
    public async Task Login_WhenCalled_ReturnsBadRequest()
    {
        //Arrange
        var model = new LoginUserDTO
        {
            Email = "email",
            Password = "password"
        };
        _accountController.ModelState.AddModelError("Email", "The Email field is not a valid e-mail address.");
        //Act
        var result = await _accountController.Login(model);
        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
    [Fact]
    public async Task Login_WhenCalled_ReturnsUnauthorized()
    {
        //Arrange
        var model = new LoginUserDTO
        {
            Email = "email",
            Password = "password"
        };
        _signInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);
        //Act
        var result = await _accountController.Login(model);
        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
