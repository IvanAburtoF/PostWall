using Microsoft.EntityFrameworkCore.Storage;
using PostWall.API.Models.EF;

namespace PostWall.API.Repositories
{
    public interface IPostRepository
    {
        Task<Post> CreatePostAsync(Post post);
        Task DeletePostAsync(int id);
        Task<Post> GetPostByIdAsync(int id);
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<Post> UpdatePostAsync(Post post);
        IDbContextTransaction BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}