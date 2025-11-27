using Microsoft.EntityFrameworkCore;
using WasteReporting.API.Data;
using WasteReporting.API.DTOs;
using WasteReporting.API.Models;

namespace WasteReporting.API.Services;

public class ManagementService : IManagementService
{
    private readonly AppDbContext _context;

    public ManagementService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CollectionPointDto> CreateCollectionPointAsync(CreateCollectionPointDto dto)
    {
        var point = new CollectionPoint { Location = dto.Location, Responsible = dto.Responsible };
        _context.CollectionPoints.Add(point);
        await _context.SaveChangesAsync();
        return new CollectionPointDto { Id = point.Id, Location = point.Location };
    }

    public async Task<RecyclerDto> CreateRecyclerAsync(CreateRecyclerDto dto)
    {
        var recycler = new Recycler { Name = dto.Name, Category = dto.Category };
        _context.Recyclers.Add(recycler);
        await _context.SaveChangesAsync();
        return new RecyclerDto { Id = recycler.Id, Name = recycler.Name };
    }

    public async Task<FinalDestinationDto> CreateFinalDestinationAsync(CreateFinalDestinationDto dto)
    {
        var destination = new FinalDestination { Description = dto.Description };
        _context.FinalDestinations.Add(destination);
        await _context.SaveChangesAsync();
        return new FinalDestinationDto { Id = destination.Id, Description = destination.Description };
    }

    public async Task<WasteDto> CreateWasteAsync(CreateWasteDto dto)
    {
        var waste = new Waste { Type = dto.Type };
        _context.Wastes.Add(waste);
        await _context.SaveChangesAsync();
        return new WasteDto { Id = waste.Id, Type = waste.Type };
    }

    public async Task<IEnumerable<WasteDto>> ListWastesAsync(int page, int pageSize)
    {
        var wastes = await _context.Wastes
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return wastes.Select(r => new WasteDto { Id = r.Id, Type = r.Type });
    }
}
