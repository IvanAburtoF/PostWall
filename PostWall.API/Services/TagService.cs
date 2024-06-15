using PostWall.API.Models.DTO;
using PostWall.API.Repositories;

namespace PostWall.API.Services;

public class TagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    

}
