using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PostWall.API.Models.EF;
using PostWall.Data;
namespace PostWall.API.Repositories;

public class MediaRepository : IMediaRepository
{
    private readonly PostWallDbContext _postWallDbContext;

    public MediaRepository(PostWallDbContext postWallDbContext)
    {
        _postWallDbContext = postWallDbContext;
    }

    public async Task<Media> CreateMediaAsync(Media media)
    {
        await _postWallDbContext.Media.AddAsync(media);
        await _postWallDbContext.SaveChangesAsync();
        return media;
    }

    public async Task<Media> UpdateMediaAsync(Media media)
    {
        _postWallDbContext.Media.Update(media);
        await _postWallDbContext.SaveChangesAsync();
        return media;
    }
    public async Task DeleteMediaAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(id);
        var media = await _postWallDbContext.Media.FindAsync(id);
        if (media == null)
        {
            throw new Exception("Media not found");
        }
        _postWallDbContext.Media.Remove(media);
        await _postWallDbContext.SaveChangesAsync();
    }
    public async Task<IEnumerable<Media>> GetMediaAsync()
    {
        return await _postWallDbContext.Media.ToListAsync();
    }
    public async Task<Media> GetMediaByIdAsync(int id)
    {
        return await _postWallDbContext.Media.FindAsync(id)?? throw new Exception("Media not found");
    }
}
