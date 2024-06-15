using PostWall.API.Models.DTO.Comment;

namespace PostWall.API.Services
{
    public interface ICommentService
    {
        Task<CommentDetailsDTO> CreateCommentAsync(CommentDetailsDTO commentDTO);
        Task DeleteCommentAsync(int id);
        Task<CommentDetailsDTO> GetCommentByIdAsync(int id);
        Task<IEnumerable<CommentDetailsDTO>> GetCommentsAsync();
        Task<CommentDetailsDTO> UpdateCommentAsync(CommentDetailsDTO commentDTO);
    }
}