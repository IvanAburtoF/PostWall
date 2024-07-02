using AutoMapper;
using PostWall.API.Models.DTO.Post;
using PostWall.API.Models.EF;
using PostWall.API.Repositories;
namespace PostWall.API.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public PostService(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
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

            throw new Exception("Error Creating post", ex);
        }
    }

    public async Task<PostDetailsDTO?> GetPostByIdAsync(int id)
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
            throw new Exception("Error Getting post", ex);
        }
    }

    public async Task<IEnumerable<PostListDTO>> GetPostsAsync(int pageNumber = 1, int pageSize = 9)
    {
        //min page number is 1
        pageNumber = Math.Max(pageNumber, 1);
        //max page size is 10
        pageSize = Math.Clamp(pageSize, 1, 10);
        try
        {
            IQueryable<Post> posts = (await _postRepository.GetPostsAsync()).AsQueryable();
            posts = posts.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return _mapper.Map<IEnumerable<PostListDTO>>(posts);

        }
        catch (AutoMapperMappingException ex)
        {
            throw new Exception("Error Mapping posts", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Error Getting posts", ex);
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
            throw new Exception("Error Updating post", ex);
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
            throw new Exception("Error Deleting post", ex);
        }
    }
    //TODO: refactor these 2 methods into one cuz DRY n' stuff
    //these 2 methods could be one
    public async Task LikePostAsync(int id, string userId)
    {
        try
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            bool userDisliked = post.DislikedBy.Any(u => u.Id == userId);
            var userLiked = post.LikedBy.Any(u => u.Id == userId);
            if (userDisliked)
            {
                var user = post.DislikedBy.First(u => u.Id == userId);
                post.DislikedBy.Remove(user);
            }
            if (userLiked)
            {
                var user = post.LikedBy.First(u => u.Id == userId);
                post.LikedBy.Remove(user);
            }
            else
            {
                var user = await _userRepository.GetUserByIdAsync(userId) ?? throw new Exception("User not found");

                post.LikedBy.Add(user);
            }

            await _postRepository.UpdatePostAsync(post);
        }
        catch (Exception ex)
        {
            throw new Exception("Error Liking post", ex);
        }
    }

    public async Task DislikePostAsync(int id, string userId)
    {
        try
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                throw new Exception("Post not found");
            }

            bool userDisliked = post.DislikedBy.Any(u => u.Id == userId);
            bool userLiked = post.LikedBy.Any(u => u.Id == userId);
            if(userLiked)
            {
                var user = post.LikedBy.First(u => u.Id == userId);
                post.LikedBy.Remove(user);
            }
            if (userDisliked)
            {
                var user = post.DislikedBy.First(u => u.Id == userId);
                post.DislikedBy.Remove(user);
            }
            else
            {
                var user = await _userRepository.GetUserByIdAsync(userId) ?? throw new Exception("User not found");

                post.DislikedBy.Add(user);
            }

            await _postRepository.UpdatePostAsync(post);
        }
        catch (Exception ex)
        {
            throw new Exception("Error Disliking post", ex);
        }
    }
}