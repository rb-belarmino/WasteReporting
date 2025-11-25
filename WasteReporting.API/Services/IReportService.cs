using WasteReporting.API.DTOs;

namespace WasteReporting.API.Services;

public interface IReportService
{
    Task<ReportResponseDto> CreateReportAsync(CreateReportDto dto, int userId);
    Task<IEnumerable<ReportResponseDto>> ListMyReportsAsync(int userId, int page, int pageSize);
    Task<IEnumerable<ReportResponseDto>> ListAllReportsAsync(int page, int pageSize);
    Task<ReportResponseDto> UpdateStatusAsync(int id, string newStatus);
}
