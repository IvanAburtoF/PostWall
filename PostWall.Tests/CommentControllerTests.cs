using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PostWall.API.Controllers;
using PostWall.API.Models.DTO.Comment;
using PostWall.API.Services;
namespace PostWall.Tests;

public class CommentControllerTests
{
    private readonly Mock<ICommentService> _commentServiceMock;
    private readonly Mock<IUserService> _userServiceMock;
    private CommentController _commentController;

    public CommentControllerTests()
    {
        _commentServiceMock = new Mock<ICommentService>();
        _userServiceMock = new Mock<IUserService>();
        _commentController = new CommentController(_commentServiceMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public async Task CreateComment_WithValidData_ReturnsCreatedComment()
    {
        // Arrange
        _userServiceMock.Setup(x => x.GetCurrentUserId()).Returns("1");

        var commentDTO = new CreateCommentDTO
        {
            Content = "Test Comment",
            PostId = 1
        };
        var createdComment = new CommentDetailsDTO
        {
            Id = 1,
            Content = commentDTO.Content
        };
        _commentServiceMock.Setup(x => x.CreateCommentAsync(commentDTO, "1")).ReturnsAsync(createdComment);
        // Act
        var result = await _commentController.CreateComment(commentDTO);
        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var model = Assert.IsType<CommentDetailsDTO>(createdAtActionResult.Value);
        Assert.Equal(createdComment.Id, model.Id);
        Assert.Equal(createdComment.Content, model.Content);
    }
    [Fact]
    public async Task CreateComment_WithInvalidData_ReturnsBadRequest()
    {
        // Arrange
        _commentController.ModelState.AddModelError("Content", "The Content field is required");
        var commentDTO = new CreateCommentDTO();
        // Act
        var result = await _commentController.CreateComment(commentDTO);
        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
    [Fact]
    public async Task CreateComment_WithInvalidUserId_ReturnsUnauthorized()
    {
        // Arrange
        _userServiceMock.Setup(x => x.GetCurrentUserId()).Returns((string)null);
        var commentDTO = new CreateCommentDTO
        {
            Content = "Test Comment",
            PostId = 1
        };
        // Act
        var result = await _commentController.CreateComment(commentDTO);
        // Assert
        Assert.IsType<UnauthorizedResult>(result.Result);
    }
    [Fact]
    public async Task GetCommentById_WithValidId_ReturnsComment()
    {
        // Arrange
        var comment = new CommentDetailsDTO
        {
            Id = 1,
            Content = "Test Comment"
        };
        _commentServiceMock.Setup(x => x.GetCommentByIdAsync(1)).ReturnsAsync(comment);
        // Act
        var result = await _commentController.GetCommentById(1);
        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsType<CommentDetailsDTO>(okObjectResult.Value);
        Assert.Equal(comment.Id, model.Id);
        Assert.Equal(comment.Content, model.Content);
    }
    [Fact]
    public async Task GetCommentById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _commentServiceMock.Setup(x => x.GetCommentByIdAsync(1)).ReturnsAsync((CommentDetailsDTO)null);
        // Act
        var result = await _commentController.GetCommentById(1);
        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
