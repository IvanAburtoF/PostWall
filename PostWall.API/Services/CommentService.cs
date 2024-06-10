using AutoMapper;
using PostWall.API.Models.DTO;
using PostWall.API.Models.EF;
using PostWall.API.Repositories;

namespace PostWall.API.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<CommentDTO> CreateCommentAsync(CommentDTO commentDTO)
    {
        try
        {
            var comment = _mapper.Map<Comment>(commentDTO);
            comment = await _commentRepository.CreateCommentAsync(comment);
            return _mapper.Map<CommentDTO>(comment);
        }
        catch (AutoMapperMappingException ex)
        {
            throw new Exception("Error Mapping comment", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating comment", ex);
        }
    }

    public async Task<CommentDTO> GetCommentByIdAsync(int id)
    {
        try
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            return _mapper.Map<CommentDTO>(comment);
        }
        catch (AutoMapperMappingException ex)
        {
            throw new Exception("Error Mapping comment", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting comment", ex);
        }
    }

    public async Task<IEnumerable<CommentDTO>> GetCommentsAsync()
    {
        try
        {
            var comments = await _commentRepository.GetCommentsAsync();
            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
        }
        catch (AutoMapperMappingException ex)
        {
            throw new Exception("Error Mapping comment", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting comments", ex);
        }
    }

    public async Task<CommentDTO> UpdateCommentAsync(CommentDTO commentDTO)
    {
        try
        {
            var comment = _mapper.Map<Comment>(commentDTO);
            comment = await _commentRepository.UpdateCommentAsync(comment);
            return _mapper.Map<CommentDTO>(comment);
        }
        catch (AutoMapperMappingException ex)
        {
            throw new Exception("Error Mapping comment", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating comment", ex);
        }
    }

    public async Task DeleteCommentAsync(int id)
    {
        try
        {
            await _commentRepository.DeleteCommentAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting comment", ex);
        }
    }
}