using Microsoft.EntityFrameworkCore;
using PostWall.API.Models.EF;
using PostWall.Data;
using System.Data.Common;

namespace PostWall.API.Repositories;

public class PostRepository : IPostRepository
{
    private readonly PostWallDbContext _postWallDbContext;

    public PostRepository(PostWallDbContext postWallDbContext)
    {
        _postWallDbContext = postWallDbContext;
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
        ArgumentNullException.ThrowIfNull(post);
        try
        {
            await _postWallDbContext.Posts.AddAsync(post);
            await _postWallDbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error creating post", ex);
        }

        return post;
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(id);
        try
        {
            var post = await _postWallDbContext.Posts
                .Include(p => p.ApplicationUser)
                .Include(p => p.Comments)
                .ThenInclude(c => c.ApplicationUser)
                .Include(p => p.Media)
                .FirstOrDefaultAsync(p => p.Id == id);
            return post ?? throw new Exception("Post not found");
        }
        catch (DbException ex)
        {
            throw new Exception("Error getting post", ex);
        }
    }

    public async Task<IEnumerable<Post>> GetPostsAsync()
    {
        try
        {
            var posts = await _postWallDbContext.Posts
                .Include(p => p.ApplicationUser)
                .Include(p => p.Media)
                .ToListAsync();
            return posts;

        }
        catch (DbException ex)
        {
            throw new Exception("Error getting posts", ex);
        }
    }

    public async Task<Post> UpdatePostAsync(Post post)
    {
        ArgumentNullException.ThrowIfNull(post);
        try
        {
            _postWallDbContext.Posts.Update(post);
            await _postWallDbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error updating post", ex);
        }

        return post;
    }

    public async Task DeletePostAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(id);
        try
        {
            var post = await _postWallDbContext.Posts.FindAsync(id);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            _postWallDbContext.Posts.Remove(post);
            await _postWallDbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error deleting post", ex);
        }
    }
}
