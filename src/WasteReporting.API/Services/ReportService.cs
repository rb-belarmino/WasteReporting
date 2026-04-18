using Microsoft.EntityFrameworkCore;
using WasteReporting.API.Data;
using WasteReporting.API.ViewModels;
using WasteReporting.API.Models;

namespace WasteReporting.API.Services;

public class ReportService : IReportService
{
    private readonly AppDbContext _context;

    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReportResponseViewModel> CreateReportAsync(CreateReportViewModel dto, int userId)
    {
        var report = new Report
        {
            UserId = userId,
            Location = dto.Location,
            Description = dto.Description,
            Status = "PENDENTE",
            CreatedAt = DateTime.UtcNow
        };

        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        // Load user to return username
        await _context.Entry(report).Reference(d => d.User).LoadAsync();

        return MapToViewModel(report);
    }

    public async Task<IEnumerable<ReportResponseViewModel>> ListMyReportsAsync(int userId, int page, int pageSize)
    {
        var reports = await _context.Reports
            .Include(d => d.User)
            .Where(d => d.UserId == userId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return reports.Select(MapToViewModel);
    }

    public async Task<IEnumerable<ReportResponseViewModel>> ListAllReportsAsync(int page, int pageSize)
    {
        var reports = await _context.Reports
            .Include(d => d.User)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return reports.Select(MapToViewModel);
    }

    public async Task<ReportResponseViewModel> UpdateStatusAsync(int id, string newStatus)
    {
        var report = await _context.Reports.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);

        if (report == null)
        {
            throw new Exception("Report not found.");
        }

        report.Status = newStatus;
        await _context.SaveChangesAsync();

        return MapToViewModel(report);
    }

    private static ReportResponseViewModel MapToViewModel(Report report)
    {
        return new ReportResponseViewModel
        {
            Id = report.Id,
            Location = report.Location,
            Description = report.Description,
            Status = report.Status,
            CreatedAt = report.CreatedAt,
            UserName = report.User?.Username ?? "Unknown"
        };
    }
}
