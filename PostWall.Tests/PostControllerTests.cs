using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PostWall.API.Controllers;
using PostWall.API.Models.DTO.Media;
using PostWall.API.Models.DTO.Post;
using PostWall.API.Models.DTO.Tag;
using PostWall.API.Services;

namespace PostWall.Tests;

public class PostControllerTests
{
    private readonly Mock<IPostService> _postServiceMock;
    private readonly Mock<IUserService> _userServiceMock;
    private PostController _postController;

    public PostControllerTests()
    {
        _postServiceMock = new Mock<IPostService>();
        _userServiceMock = new Mock<IUserService>();
        _postController = new PostController(_postServiceMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public async Task CreatePost_WithValidData_ReturnsCreatedPost()
    {
        // Arrange
        
        _userServiceMock.Setup(x => x.GetCurrentUserId()).Returns("1");

        var postDTO = new CreatePostDTO
        {
            Title = "Test Post",
            Media = new CreateMediaDTO
            {
                Url = "https://example.com/image.jpg",
                Type = "Jpg"
            },
            Tags = new List<CreateTagDTO>
            {
                new CreateTagDTO { Name = "Test Tag" }
            }
        };
        var createdPost = new PostDetailsDTO
        {
            Id = 1,
            Title = postDTO.Title,
            Media = new MediaDTO
            {
                Url = postDTO.Media.Url,
                Type = postDTO.Media.Type
            },
            Tags = postDTO.Tags.Select(x => new TagDTO { Name = x.Name }).ToList()
        };
        _postServiceMock.Setup(x => x.CreatePostAsync(postDTO, "1")).ReturnsAsync(createdPost);
        // Act
        var result = await _postController.CreatePost(postDTO);
        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var post = Assert.IsType<PostDetailsDTO>(createdAtActionResult.Value);
        Assert.Equal(postDTO.Title, post.Title);
        Assert.Equal(postDTO.Media.Url, post.Media.Url);
        Assert.Equal(postDTO.Media.Type, post.Media.Type);
        Assert.Equal(postDTO.Tags.Count, post?.Tags?.Count);
        Assert.Equal(postDTO.Tags.First().Name, post?.Tags?.First().Name);

        _postServiceMock.Verify(x => x.CreatePostAsync(postDTO, It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task CreatePost_WithInvalidData_ReturnsBadRequest()
    {
        // Arrange
        _postController.ModelState.AddModelError("Title", "Title is required");
        var postDTO = new CreatePostDTO();
        // Act
        var result = await _postController.CreatePost(postDTO);
        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreatePost_WithInvalidUserId_ReturnsUnauthorized()
    {
        // Arrange
        _userServiceMock.Setup(x => x.GetCurrentUserId()).Returns((string?)null);
        var postDTO = new CreatePostDTO();
        // Act
        var result = await _postController.CreatePost(postDTO);
        // Assert
        Assert.IsType<UnauthorizedResult>(result.Result);
    }

    [Fact]
    public async Task GetPostById_WithValidId_ReturnsPost()
    {
        // Arrange
        var post = new PostDetailsDTO
        {
            Id = 1,
            Title = "Test Post",
            Media = new MediaDTO
            {
                Url = "https://example.com/image.jpg",
                Type = "Jpg"
            },
            Tags = new List<TagDTO>
            {
                new TagDTO { Name = "Test Tag" }
            }
        };
        _postServiceMock.Setup(x => x.GetPostByIdAsync(1)).ReturnsAsync(post);
        // Act
        var result = await _postController.GetPostById(1);
    }

    [Fact]
    public async Task GetPosts_ReturnsPosts()
    {
        // Arrange
        var posts = new List<PostListDTO>
        {
            new PostListDTO
            {
                Id = 1,
                Title = "Test Post",
                Media = new MediaDTO
                {
                    Url = "https://example.com/image.jpg",
                    Type = "Jpg"
                },
                Tags = new List<TagDTO>
                {
                    new TagDTO { Name = "Test Tag" }
                }
            }
        };
        _postServiceMock.Setup(x => x.GetPostsAsync(1,9)).ReturnsAsync(posts);
        // Act
        var result = await _postController.GetPosts();
    }
    [Fact]
    public async Task GetPosts_ReturnsInternalServerError()
    {
        // Arrange
        _postServiceMock.Setup(x => x.GetPostsAsync(1,9)).ThrowsAsync(new Exception("An error occurred"));
        // Act
        var result = await _postController.GetPosts();
        // Assert
        Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, (result.Result as ObjectResult)?.StatusCode);
    }
    [Fact]
    public async Task GetPostById_ReturnsInternalServerError()
    {
        // Arrange
        _postServiceMock.Setup(x => x.GetPostByIdAsync(1)).ThrowsAsync(new Exception("An error occurred"));
        // Act
        var result = await _postController.GetPostById(1);
        // Assert
        Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, (result.Result as ObjectResult)?.StatusCode);
    }
    [Fact]
    public async Task CreatePost_ReturnsInternalServerError()
    {
        // Arrange
        _userServiceMock.Setup(x => x.GetCurrentUserId()).Returns("1");
        _postServiceMock.Setup(x => x.CreatePostAsync(It.IsAny<CreatePostDTO>(), "1")).ThrowsAsync(new Exception("An error occurred"));
        var postDTO = new CreatePostDTO
        {
            Title = "Test Post",
            Media = new CreateMediaDTO
            {
                Url = "https://example.com/image.jpg",
                Type = "Jpg"
            },
            Tags = new List<CreateTagDTO>
            {
                new CreateTagDTO { Name = "Test Tag" }
            }
        };
        // Act
        var result = await _postController.CreatePost(postDTO);
        // Assert
        Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, (result.Result as ObjectResult)?.StatusCode);
    }
}
