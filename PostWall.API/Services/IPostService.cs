using PostWall.API.Models.DTO.Post;

namespace PostWall.API.Services
{
    public interface IPostService
    {
        Task<PostDetailsDTO> CreatePostAsync(CreatePostDTO postDTO, string userId);
        Task DeletePostAsync(int id, string userId);
        Task<PostDetailsDTO> GetPostByIdAsync(int id);
        Task<IEnumerable<PostListDTO>> GetPostsAsync( int pageNumber = 1, int pageSize = 9);
        Task<PostDetailsDTO> UpdatePostAsync(UpdatePostDTO postDTO, string userId);
        Task LikePostAsync(int id, string userId);
        Task DislikePostAsync(int id, string userId);
    }
}