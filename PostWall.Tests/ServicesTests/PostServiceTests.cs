
using AutoMapper;
using Moq;
using PostWall.API.Models.DTO.Media;
using PostWall.API.Models.DTO.Post;
using PostWall.API.Models.DTO.Tag;
using PostWall.API.Models.EF;
using PostWall.API.Repositories;
using PostWall.API.Services;

namespace PostWall.Tests.ServicesTests;

public class PostServiceTests
{
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public PostServiceTests()
    {
        _postRepositoryMock = new Mock<IPostRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
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
        var postRepositoryMock = new Mock<IPostRepository>();
        postRepositoryMock.Setup(x => x.CreatePostAsync(It.IsAny<Post>())).ReturnsAsync(createdPost);
        var userRepositoryMock = new Mock<IUserRepository>();
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Post>(postDTO)).Returns(post);
        mapperMock.Setup(x => x.Map<PostDetailsDTO>(createdPost)).Returns(postDetailsDTO);
        var postService = new PostService(postRepositoryMock.Object, userRepositoryMock.Object, mapperMock.Object);
        // Act
        var result = await postService.CreatePostAsync(postDTO, "1");
        // Assert
        Assert.IsType<PostDetailsDTO>(result);
        Assert.Equal(createdPost.Id, result.Id);
    }

    [Fact]
    public async Task CreatePostAsync_WithInvalidData_ReturnsMappingError()
    {
        // Arrange
        var postDTO = new CreatePostDTO();
        var postRepositoryMock = new Mock<IPostRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Post>(postDTO)).Throws<AutoMapperMappingException>();
        var postService = new PostService(postRepositoryMock.Object, userRepositoryMock.Object, mapperMock.Object);
        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => postService.CreatePostAsync(postDTO, "1"));
        // Assert
        Assert.Equal("Error Mapping post", exception.Message);
    }
}
