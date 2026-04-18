using WasteReporting.API.ViewModels;

namespace WasteReporting.API.Services;

public interface IReportService
{
    Task<ReportResponseViewModel> CreateReportAsync(CreateReportViewModel dto, int userId);
    Task<IEnumerable<ReportResponseViewModel>> ListMyReportsAsync(int userId, int page, int pageSize);
    Task<IEnumerable<ReportResponseViewModel>> ListAllReportsAsync(int page, int pageSize);
    Task<ReportResponseViewModel> UpdateStatusAsync(int id, string newStatus);
}
