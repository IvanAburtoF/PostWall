using PostWall.API.Models.EF;

namespace PostWall.API.Repositories
{
    public interface IMediaRepository
    {
        Task<Media> CreateMediaAsync(Media media);
        Task DeleteMediaAsync(int id);
        Task<IEnumerable<Media>> GetMediaAsync();
        Task<Media> GetMediaByIdAsync(int id);
        Task<Media> UpdateMediaAsync(Media media);
    }
}