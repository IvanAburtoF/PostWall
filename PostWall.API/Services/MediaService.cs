using PostWall.API.Models.DTO;
using PostWall.API.Repositories;

namespace PostWall.API.Services;

public class MediaService
{
    private readonly MediaRepository _mediaRepository;

    public MediaService(MediaRepository mediaRepository)
    {
        _mediaRepository = mediaRepository;
    }

    public async Task<MediaDTO> CreateMediaAsync(MediaDTO mediaDTO)
    {
        return await _mediaRepository.CreateMediaAsync(mediaDTO);
    }

    public async Task<MediaDTO> GetMediaByIdAsync(int id)
    {
        return await _mediaRepository.GetMediaByIdAsync(id);
    }

    public async Task<IEnumerable<MediaDTO>> GetMediaAsync()
    {
        return await _mediaRepository.GetMediaAsync();
    }

    public async Task<MediaDTO> UpdateMediaAsync(MediaDTO mediaDTO)
    {
        return await _mediaRepository.UpdateMediaAsync(mediaDTO);
    }

    public async Task DeleteMediaAsync(int id)
    {
        await _mediaRepository.DeleteMediaAsync(id);
    }
}
