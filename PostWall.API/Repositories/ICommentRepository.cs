using PostWall.API.Models.EF;

namespace PostWall.API.Repositories;

public interface ICommentRepository
{
    Task<Comment> CreateCommentAsync(Comment comment);
    Task DeleteCommentAsync(int id);
    Task<Comment> GetCommentByIdAsync(int id);
    Task<IEnumerable<Comment>> GetCommentsAsync();
    Task<Comment> UpdateCommentAsync(Comment comment);
}