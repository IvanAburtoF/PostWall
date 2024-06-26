using PostWall.API.Models.DTO.Comment;

namespace PostWall.API.Services
{
    public interface ICommentService
    {
        Task<CommentDetailsDTO> CreateCommentAsync(CreateCommentDTO CreateCommentDTO, string userId);
        Task DeleteCommentAsync(int id, string userId);
        Task<CommentDetailsDTO> GetCommentByIdAsync(int id);
        Task<IEnumerable<CommentDetailsDTO>> GetCommentsAsync();
    }
}