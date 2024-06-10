using PostWall.API.Models.DTO;
using PostWall.API.Repositories;

namespace PostWall.API.Services;

public class ReportService
{
    private readonly ReportRepository _reportRepository;

    public ReportService(ReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<ReportDTO> CreateReportAsync(ReportDTO reportDTO)
    {
        return await _reportRepository.CreateReportAsync(reportDTO);
    }

    public async Task<ReportDTO> GetReportByIdAsync(int id)
    {
        return await _reportRepository.GetReportByIdAsync(id);
    }

    public async Task<IEnumerable<ReportDTO>> GetReportsAsync()
    {
        return await _reportRepository.GetReportsAsync();
    }

    public async Task<ReportDTO> UpdateReportAsync(ReportDTO reportDTO)
    {
        return await _reportRepository.UpdateReportAsync(reportDTO);
    }

    public async Task DeleteReportAsync(int id)
    {
        await _reportRepository.DeleteReportAsync(id);
    }
}
