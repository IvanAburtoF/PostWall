using Microsoft.EntityFrameworkCore;
using PostWall.API.Models.EF;
using PostWall.Data;
using System.Data.Common;

namespace PostWall.API.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly PostWallDbContext _postWallDbContext;    

    public CommentRepository(PostWallDbContext postWallDbContext)
    {
        _postWallDbContext = postWallDbContext;
    }

    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
        ArgumentNullException.ThrowIfNull(comment);
        try
        {
            await _postWallDbContext.Comments.AddAsync(comment);
            await _postWallDbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error creating comment", ex);
        }

        return comment;
    }

    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(id);
        try
        {
            var comment = await _postWallDbContext.Comments
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(c => c.Id == id);
            return comment ?? throw new Exception("Comment not found");
        }
        catch (DbException ex)
        {
            throw new Exception("Error getting comment", ex);
        }
    }

    public async Task<IEnumerable<Comment>> GetCommentsAsync()
    {
        try
        {
            var comments = await _postWallDbContext.Comments
                .Include(c => c.ApplicationUser)
                .ToListAsync();
            return comments;

        }
        catch (DbException ex)
        {
            throw new Exception("Error getting comments", ex);
        }
    }

    public async Task<Comment> UpdateCommentAsync(Comment comment)
    {
        ArgumentNullException.ThrowIfNull(comment);
        try
        {
        _postWallDbContext.Comments.Update(comment);
        await _postWallDbContext.SaveChangesAsync();
        return comment;
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error updating comment", ex);
        }
    }

    public async Task DeleteCommentAsync(int id)
    {
        var comment = await _postWallDbContext.Comments.FindAsync(id);
        if (comment == null)
        {
            throw new Exception("Comment not found");
        }
        try
        {
            _postWallDbContext.Comments.Remove(comment);
            await _postWallDbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error deleting comment", ex);
        }        
    }
}
