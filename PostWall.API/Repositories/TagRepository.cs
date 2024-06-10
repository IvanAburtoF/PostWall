using Microsoft.EntityFrameworkCore;
using PostWall.API.Models.EF;
using PostWall.Data;
namespace PostWall.API.Repositories;

public class TagRepository : ITagRepository
{
    private readonly PostWallDbContext _postWallDbContext;

    public TagRepository(PostWallDbContext postWallDbContext)
    {
        _postWallDbContext = postWallDbContext;
    }

    public async Task<Tag> CreateTagAsync(Tag tag)
    {
        var tag = _mapper.Map<Tag>(tag);
        await _postWallDbContext.Tags.AddAsync(tag);
        await _postWallDbContext.SaveChangesAsync();
        return _mapper.Map<Tag>(tag);
    }

    public async Task<Tag> GetTagByIdAsync(int id)
    {
        var tag = await _postWallDbContext.Tags
            .FirstOrDefaultAsync(t => t.Id == id);
        return _mapper.Map<Tag>(tag);
    }

    public async Task<IEnumerable<Tag>> GetTagsAsync()
    {
        var tags = await _postWallDbContext.Tags
            .ToListAsync();
        return _mapper.Map<IEnumerable<Tag>>(tags);
    }

    public async Task<Tag> UpdateTagAsync(Tag tag)
    {
        var tag = _mapper.Map<Tag>(tag);
        _postWallDbContext.Tags.Update(tag);
        await _postWallDbContext.SaveChangesAsync();
        return _mapper.Map<Tag>(tag);
    }

    public async Task DeleteTagAsync(int id)
    {
        var tag = await _postWallDbContext.Tags.FindAsync(id);
        if (tag == null)
        {
            throw new Exception("Tag not found");
        }
        _postWallDbContext.Tags.Remove(tag);
        await _postWallDbContext.SaveChangesAsync();
    }
}
