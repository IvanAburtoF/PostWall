using PostWall.API.Models.EF;
using PostWall.Data;
using System.Data.Common;

namespace PostWall.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PostWallDbContext _postWallDBContext;
    public UserRepository(PostWallDbContext context)
    {
        _postWallDBContext = context;
    }
    public async Task<ApplicationUser> GetUserByIdAsync(string id)
    {
        try
        {
            return await _postWallDBContext.Users.FindAsync(id);
        }
        catch (DbException ex)
        {
            throw new Exception("Error getting user", ex);
        }
    }
}
