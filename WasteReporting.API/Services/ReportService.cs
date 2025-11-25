using Microsoft.EntityFrameworkCore;
using WasteReporting.API.Data;
using WasteReporting.API.DTOs;
using WasteReporting.API.Models;

namespace WasteReporting.API.Services;

public class ReportService : IReportService
{
    private readonly AppDbContext _context;

    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReportResponseDto> CreateReportAsync(CreateReportDto dto, int userId)
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

        return MapToDto(report);
    }

    public async Task<IEnumerable<ReportResponseDto>> ListMyReportsAsync(int userId, int page, int pageSize)
    {
        var reports = await _context.Reports
            .Include(d => d.User)
            .Where(d => d.UserId == userId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return reports.Select(MapToDto);
    }

    public async Task<IEnumerable<ReportResponseDto>> ListAllReportsAsync(int page, int pageSize)
    {
        var reports = await _context.Reports
            .Include(d => d.User)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return reports.Select(MapToDto);
    }

    public async Task<ReportResponseDto> UpdateStatusAsync(int id, string newStatus)
    {
        var report = await _context.Reports.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);

        if (report == null)
        {
            throw new Exception("Report not found.");
        }

        report.Status = newStatus;
        await _context.SaveChangesAsync();

        return MapToDto(report);
    }

    private static ReportResponseDto MapToDto(Report report)
    {
        return new ReportResponseDto
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
