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

    public async Task<PostDetailsDTO> UpdatePostAsync(UpdatePostDTO postDTO, string userId)
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

    public async Task DeletePostAsync(int id, string userId)
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

    public async Task LikePostAsync(int id, string userId)
    {
        try
        {
            var post =await _postRepository.GetPostByIdAsync(id);
            if(post == null)
            {
                throw new Exception("Post not found");
            }
            /*maybe i should allow the owner to like his own post
            for testing i need it
            if(post.UserId == userId)
            {
                throw new Exception("You can't like your own post");
            }*/
            if(post.LikedBy.Any(l => l.Id == userId))
            {
              return;
            }
            if(post.DislikedBy.Any(l => l.Id == userId))
            {
                post.DislikedBy.Remove(post.DislikedBy.First(l => l.Id == userId));
            }

            await _postRepository.LikePostAsync(id, userId);
        }
        catch (Exception ex)
        {
            throw new Exception("Error liking post", ex);
        }
    }

    public async Task DislikePostAsync(int id, string userId)
    {
        try
        {
            await _postRepository.DislikePostAsync(id, userId);
        }
        catch (Exception ex)
        {
            throw new Exception("Error disliking post", ex);
        }
    }
}