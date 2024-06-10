using PostWall.API.Models.DTO;
using PostWall.API.Repositories;

namespace PostWall.API.Services;

public class TagService
{
    private readonly TagRepository _tagRepository;

    public TagService(TagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<TagDTO> CreateTagAsync(TagDTO tagDTO)
    {
        return await _tagRepository.CreateTagAsync(tagDTO);
    }

    public async Task<TagDTO> GetTagByIdAsync(int id)
    {
        return await _tagRepository.GetTagByIdAsync(id);
    }

    public async Task<IEnumerable<TagDTO>> GetTagsAsync()
    {
        return await _tagRepository.GetTagsAsync();
    }

    public async Task<TagDTO> UpdateTagAsync(TagDTO tagDTO)
    {
        return await _tagRepository.UpdateTagAsync(tagDTO);
    }

    public async Task DeleteTagAsync(int id)
    {
        await _tagRepository.DeleteTagAsync(id);
    }
}
