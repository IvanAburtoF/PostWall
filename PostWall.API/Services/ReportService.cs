using PostWall.API.Models.DTO;
using PostWall.API.Repositories;

namespace PostWall.API.Services;

public class ReportService
{
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

}
