using AutoMapper;
using PostWall.API.Models.DTO;
using PostWall.API.Models.EF;
using PostWall.API.Repositories;
using System.Transactions;
namespace PostWall.API.Services;

public class PostService
{
    private readonly IPostRepository _postRepository;
    private readonly IMediaRepository _mediaRepository;
    private readonly IMapper _mapper;

    public PostService(IPostRepository postRepository,IMediaRepository mediaRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _mediaRepository = mediaRepository;
        _mapper = mapper;
    }

    public async Task<PostDTO> CreatePostAsync(PostDTO postDTO)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        try
        {
            // Insert the media
            var media = _mapper.Map<Media>(postDTO.Media);
            media = await _mediaRepository.CreateMediaAsync(media);

            // Insert the post
            var post = _mapper.Map<Post>(postDTO);
            post.Media.Id = media.Id;
            post = await _postRepository.CreatePostAsync(post);

            // Commit the transaction
            transaction.Complete();

            return _mapper.Map<PostDTO>(post);
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

    public async Task<PostDTO> GetPostByIdAsync(int id)
    {
        try
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            return _mapper.Map<PostDTO>(post);
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

    public async Task<IEnumerable<PostDTO>> GetPostsAsync()
    {
        try
        {
            var posts = await _postRepository.GetPostsAsync();
            return _mapper.Map<IEnumerable<PostDTO>>(posts);
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

    public async Task<PostDTO> UpdatePostAsync(PostDTO postDTO)
    {
        try
        {
            var post = _mapper.Map<Post>(postDTO);
            post = await _postRepository.UpdatePostAsync(post);
            return _mapper.Map<PostDTO>(post);
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