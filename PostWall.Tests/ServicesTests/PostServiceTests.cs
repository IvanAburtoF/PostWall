
using AutoMapper;
using Moq;
using PostWall.API.Models.DTO.Media;
using PostWall.API.Models.DTO.Post;
using PostWall.API.Models.DTO.Tag;
using PostWall.API.Models.EF;
using Microsoft.AspNetCore.Identity;
using PostWall.API.Repositories;
using PostWall.API.Services;

namespace PostWall.Tests.ServicesTests;

public class PostServiceTests
{
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly PostService _postService;

    public PostServiceTests()
    {
        _postRepositoryMock = new Mock<IPostRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _postService = new PostService(_postRepositoryMock.Object, _userRepositoryMock.Object, _mapperMock.Object);
    }


    [Fact]
    public async Task CreatePostAsync_WithValidData_ReturnsCreatedPost()
    {
        // Arrange
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
        var post = new Post
        {
            Title = postDTO.Title,
            Media = new Media
            {
         
                Url = postDTO.Media.Url,
                Type = MediaType.Jpg
            },
            Tags = new List<Tag>
            {
                new Tag { Name = "Test Tag" }
            }
        };
        var createdPost = post;
        createdPost.Id = 1;
        createdPost.Media.Id = 1;
        createdPost.Tags.First().Id = 1;
        var postDetailsDTO = new PostDetailsDTO
        {
            Id= post.Id,
            Title = "Test Post",
            Media = new MediaDTO
            {
                Id = 1,
                Url = "https://example.com/image.jpg",
                Type = "Jpg"
            },
            Tags = new List<TagDTO>
            {
                new TagDTO { Id = 1,Name = "Test Tag" }
            }
        };        
        _postRepositoryMock.Setup(x => x.CreatePostAsync(It.IsAny<Post>())).ReturnsAsync(createdPost);                
        _mapperMock.Setup(x => x.Map<Post>(postDTO)).Returns(post);
        _mapperMock.Setup(x => x.Map<PostDetailsDTO>(createdPost)).Returns(postDetailsDTO);
        // Act
        var result = await _postService.CreatePostAsync(postDTO, "1");
        // Assert
        Assert.IsType<PostDetailsDTO>(result);
        Assert.Equal(createdPost.Id, result.Id);
    }

    [Fact]
    public async Task CreatePostAsync_WithInvalidData_ReturnsMappingError()
    {
        // Arrange
        var postDTO = new CreatePostDTO();
        _mapperMock.Setup(x => x.Map<Post>(postDTO)).Throws<AutoMapperMappingException>();

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _postService.CreatePostAsync(postDTO, "1"));
        // Assert
        Assert.Equal("Error Mapping post", exception.Message);
    }

    [Fact]
    public async Task CreatePostAsync_WithRepositoryError_ReturnsRepositoryError()
    {
        // Arrange
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
        var post = new Post
        {
            Title = postDTO.Title,
            Media = new Media
            {
                Url = postDTO.Media.Url,
                Type = MediaType.Jpg
            },
            Tags = new List<Tag>
            {
                new Tag { Name = "Test Tag" }
            }
        };
        
        _postRepositoryMock.Setup(x => x.CreatePostAsync(It.IsAny<Post>())).Throws<Exception>();        
        _mapperMock.Setup(x => x.Map<Post>(postDTO)).Returns(post);

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _postService.CreatePostAsync(postDTO, "1"));
        // Assert
        Assert.Equal("Error Creating post", exception.Message);
    }

    [Fact]
    public async Task GetPostByIdAsync_WithValidData_ReturnsPost()
    {
        // Arrange
        var post = new Post
        {
            Id = 1,
            Title = "Test Post",
            Media = new Media
            {
                Id = 1,
                Url = "https://example.com/image.jpg",
                Type = MediaType.Jpg
            },
            Tags = new List<Tag>
            {
                new Tag { Id = 1, Name = "Test Tag" }
            }
        };
        var postDetailsDTO = new PostDetailsDTO
        {
            Id = post.Id,
            Title = "Test Post",
            Media = new MediaDTO
            {
                Id = 1,
                Url = "https://example.com/image.jpg",
                Type = "Jpg"
            },
            Tags = new List<TagDTO>
            {
                new TagDTO { Id = 1, Name = "Test Tag" }
            }
        };        
        _postRepositoryMock.Setup(x => x.GetPostByIdAsync(1)).ReturnsAsync(post);        
        _mapperMock.Setup(x => x.Map<PostDetailsDTO>(post)).Returns(postDetailsDTO);

        // Act
        var result = await _postService.GetPostByIdAsync(1);
        // Assert
        Assert.IsType<PostDetailsDTO>(result);
        Assert.Equal(post.Id, result.Id);
    }

    [Fact]
    public async Task GetPostByIdAsync_WithRepositoryError_ReturnsRepositoryError()
    {
        // Arrange
        
        _postRepositoryMock.Setup(x => x.GetPostByIdAsync(1)).Throws<Exception>();        

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _postService.GetPostByIdAsync(1));
        // Assert
        Assert.Equal("Error Getting post", exception.Message);
    }
    [Fact]
    public async Task GetPostsAsync_WithValidData_ReturnsPosts()
    {
        // Arrange
        var posts = new List<Post>
        {
            new Post
            {
                Id = 1,
                Title = "Test Post",
                Media = new Media
                {
                    Id = 1,
                    Url = "https://example.com/image.jpg",
                    Type = MediaType.Jpg
                },
                Tags = new List<Tag>
                {
                    new Tag { Id = 1, Name = "Test Tag" }
                }
            }
        };
        var PostListDTO = new List<PostListDTO>
        {
            new PostListDTO
            {
                Id = posts.First().Id,
                Title = "Test Post",
                Media = new MediaDTO
                {
                    Id = 1,
                    Url = "https://example.com/image.jpg",
                    Type = "Jpg"
                },
                Tags = new List<TagDTO>
                {
                    new TagDTO { Id = 1, Name = "Test Tag" }
                }
            }
        };
        _postRepositoryMock.Setup(x => x.GetPostsAsync()).ReturnsAsync(posts);        
        _mapperMock.Setup(x => x.Map<IEnumerable<PostListDTO>>(posts)).Returns(PostListDTO);

        // Act
        var result = await _postService.GetPostsAsync();
        //Assert
        Assert.IsAssignableFrom<IEnumerable<PostListDTO>>(result);
        Assert.Equal(posts.First().Id, result.First().Id);
    }
    [Fact]
    public async Task GetPostsAsync_WithRepositoryError_ReturnsRepositoryError()
    {
        // Arrange
        
        _postRepositoryMock.Setup(x => x.GetPostsAsync()).Throws<Exception>();
        

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _postService.GetPostsAsync());
        // Assert
        Assert.Equal("Error Getting posts", exception.Message);
    }
    [Fact]
    public async Task LikePostAsync_WithCorrectData_AddLikeUser()
    {
        // Arrange
        var post = new Post
        {
            Id = 1,
            Title = "Test Post",
            Media = new Media
            {
                Id = 1,
                Url = "https://example.com/image.jpg",
                Type = MediaType.Jpg
            },
            Tags = new List<Tag>
            {
                new Tag { Id = 1, Name = "Test Tag" }
            }
        };
        var user = new ApplicationUser
        {
            Id = "1",
            UserName = "Test User"
        };
        _postRepositoryMock.Setup(x => x.GetPostByIdAsync(1)).ReturnsAsync(post);
        _postRepositoryMock.Setup(x => x.UpdatePostAsync(post)).ReturnsAsync(post).Verifiable("Not called");
        _userRepositoryMock.Setup(x => x.GetUserByIdAsync("1")).ReturnsAsync(user);
        
        // Act
        await _postService.LikePostAsync(1, "1");
        // Assert
        Assert.Contains(user, post.LikedBy);
        _postRepositoryMock.Verify(x => x.UpdatePostAsync(post), Times.Once);
    }
}
