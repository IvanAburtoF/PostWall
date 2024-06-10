using PostWall.API.Models.DTO;

namespace PostWall.API.Services
{
    public interface ICommentService
    {
        Task<CommentDTO> CreateCommentAsync(CommentDTO commentDTO);
        Task DeleteCommentAsync(int id);
        Task<CommentDTO> GetCommentByIdAsync(int id);
        Task<IEnumerable<CommentDTO>> GetCommentsAsync();
        Task<CommentDTO> UpdateCommentAsync(CommentDTO commentDTO);
    }
}