using PostWall.API.Models.EF;
using PostWall.Data;

namespace PostWall.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PostWallDbContext _context;
    public UserRepository(PostWallDbContext context)
    {
        _context = context;
    }
    public async Task<ApplicationUser> GetUserByIdAsync(string id)
    {
        return await _context.Users.FindAsync(id);
    }
}
