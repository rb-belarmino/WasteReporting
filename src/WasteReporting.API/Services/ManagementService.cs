using Microsoft.EntityFrameworkCore;
using WasteReporting.API.Data;
using WasteReporting.API.ViewModels;
using WasteReporting.API.Models;

namespace WasteReporting.API.Services;

public class ManagementService : IManagementService
{
    private readonly AppDbContext _context;

    public ManagementService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CollectionPointViewModel> CreateCollectionPointAsync(CreateCollectionPointViewModel dto)
    {
        var point = new CollectionPoint { Location = dto.Location, Responsible = dto.Responsible };
        _context.CollectionPoints.Add(point);
        await _context.SaveChangesAsync();
        return new CollectionPointViewModel { Id = point.Id, Location = point.Location };
    }

    public async Task<RecyclerViewModel> CreateRecyclerAsync(CreateRecyclerViewModel dto)
    {
        var recycler = new Recycler { Name = dto.Name, Category = dto.Category };
        _context.Recyclers.Add(recycler);
        await _context.SaveChangesAsync();
        return new RecyclerViewModel { Id = recycler.Id, Name = recycler.Name };
    }

    public async Task<FinalDestinationViewModel> CreateFinalDestinationAsync(CreateFinalDestinationViewModel dto)
    {
        var destination = new FinalDestination { Description = dto.Description };
        _context.FinalDestinations.Add(destination);
        await _context.SaveChangesAsync();
        return new FinalDestinationViewModel { Id = destination.Id, Description = destination.Description };
    }

    public async Task<WasteViewModel> CreateWasteAsync(CreateWasteViewModel dto)
    {
        var waste = new Waste { Type = dto.Type };
        _context.Wastes.Add(waste);
        await _context.SaveChangesAsync();
        return new WasteViewModel { Id = waste.Id, Type = waste.Type };
    }

    public async Task<IEnumerable<WasteViewModel>> ListWastesAsync(int page, int pageSize)
    {
        var wastes = await _context.Wastes
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return wastes.Select(r => new WasteViewModel { Id = r.Id, Type = r.Type });
    }
}
