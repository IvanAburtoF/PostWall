using AutoMapper;
using PostWall.API.Models.DTO.Post;
using PostWall.API.Models.EF;
using PostWall.API.Repositories;
namespace PostWall.API.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public PostService(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _mapper = mapper;
    }

    public async Task<PostDetailsDTO> CreatePostAsync(CreatePostDTO postDTO, string userId)
    {
        try
        {
            var post = _mapper.Map<Post>(postDTO);
            post.UserId = userId;
            post = await _postRepository.CreatePostAsync(post);
            return _mapper.Map<PostDetailsDTO>(post);
        }
        catch (AutoMapperMappingException ex)
        {

            throw new Exception("Error Mapping post", ex);
        }
        catch (Exception ex)
        {

            throw new Exception("Error creating post", ex);
        }
    }

    public async Task<PostDetailsDTO> GetPostByIdAsync(int id)
    {
        try
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            return _mapper.Map<PostDetailsDTO>(post);
        }
        catch (AutoMapperMappingException ex)
        {
            throw new Exception("Error Mapping post", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting post", ex);
        }
    }

    public async Task<IEnumerable<PostListDTO>> GetPostsAsync()
    {
        try
        {
            var posts = await _postRepository.GetPostsAsync();
            return _mapper.Map<IEnumerable<PostListDTO>>(posts);
        }
        catch (AutoMapperMappingException ex)
        {
            throw new Exception("Error Mapping posts", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting posts", ex);
        }
    }

    public async Task<PostDetailsDTO> UpdatePostAsync(UpdatePostDTO postDTO)
    {
        try
        {
            var post = _mapper.Map<Post>(postDTO);
            post = await _postRepository.UpdatePostAsync(post);
            return _mapper.Map<PostDetailsDTO>(post);
        }
        catch (AutoMapperMappingException ex)
        {
            throw new Exception("Error Mapping post", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating post", ex);
        }
    }

    public async Task DeletePostAsync(int id)
    {
        try
        {
            await _postRepository.DeletePostAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting post", ex);
        }
    }
}