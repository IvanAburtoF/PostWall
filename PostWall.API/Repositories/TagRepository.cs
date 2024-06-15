using PostWall.API.Models.EF;

namespace PostWall.API.Repositories;

public class TagRepository : ITagRepository
{
    public Task<Tag> CreateTagAsync(Tag tag)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTagAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Tag> GetTagByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Tag>> GetTagsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Tag> UpdateTagAsync(Tag tag)
    {
        throw new NotImplementedException();
    }
}
