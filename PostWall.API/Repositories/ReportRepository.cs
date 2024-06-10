using Microsoft.EntityFrameworkCore;
using PostWall.API.Models.EF;
using PostWall.Data;
namespace PostWall.API.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly PostWallDbContext _postWallDbContext;

    public ReportRepository(PostWallDbContext postWallDbContext)
    {
        _postWallDbContext = postWallDbContext;
    }

    public async Task<Report> CreateReportAsync(Report report)
    {
        var report = _mapper.Map<Report>(report);
        await _postWallDbContext.Reports.AddAsync(report);
        await _postWallDbContext.SaveChangesAsync();
        return _mapper.Map<Report>(report);
    }

    public async Task<Report> GetReportByIdAsync(int id)
    {
        var report = await _postWallDbContext.Reports
            .Include(r => r.ApplicationUser)
            .FirstOrDefaultAsync(r => r.Id == id);
        return _mapper.Map<Report>(report);
    }

    public async Task<IEnumerable<Report>> GetReportsAsync()
    {
        var reports = await _postWallDbContext.Reports
            .Include(r => r.ApplicationUser)
            .ToListAsync();
        return _mapper.Map<IEnumerable<Report>>(reports);
    }

    public async Task<Report> UpdateReportAsync(Report report)
    {
        var report = _mapper.Map<Report>(report);
        _postWallDbContext.Reports.Update(report);
        await _postWallDbContext.SaveChangesAsync();
        return _mapper.Map<Report>(report);
    }

    public async Task DeleteReportAsync(int id)
    {
        var report = await _postWallDbContext.Reports.FindAsync(id);
        if (report == null)
        {
            throw new Exception("Report not found");
        }
        _postWallDbContext.Reports.Remove(report);
        await _postWallDbContext.SaveChangesAsync();
    }
}
