using PostWall.API.Models.DTO.Post;

namespace PostWall.API.Services
{
    public interface IPostService
    {
        Task<PostDetailsDTO> CreatePostAsync(CreatePostDTO postDTO, string userId);
        Task DeletePostAsync(int id);
        Task<PostDetailsDTO> GetPostByIdAsync(int id);
        Task<IEnumerable<PostListDTO>> GetPostsAsync();
        Task<PostDetailsDTO> UpdatePostAsync(UpdatePostDTO postDTO);
    }
}