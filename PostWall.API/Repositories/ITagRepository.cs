using PostWall.API.Models.EF;

namespace PostWall.API.Repositories
{
    public interface ITagRepository
    {
        Task<Tag> CreateTagAsync(Tag tag);
        Task DeleteTagAsync(int id);
        Task<Tag> GetTagByIdAsync(int id);
        Task<IEnumerable<Tag>> GetTagsAsync();
        Task<Tag> UpdateTagAsync(Tag tag);
    }
}