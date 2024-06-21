using PostWall.API.Models.EF;

namespace PostWall.API.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser> GetUserByIdAsync(string id);
}