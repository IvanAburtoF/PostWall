using PostWall.API.Models.EF;

namespace PostWall.API.Repositories
{
    public interface IReportRepository
    {
        Task<Report> CreateReportAsync(Report report);
        Task DeleteReportAsync(int id);
        Task<Report> GetReportByIdAsync(int id);
        Task<IEnumerable<Report>> GetReportsAsync();
        Task<Report> UpdateReportAsync(Report report);
    }
}