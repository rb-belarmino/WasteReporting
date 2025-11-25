using Microsoft.EntityFrameworkCore;
using WasteReporting.API.Data;
using WasteReporting.API.DTOs;
using WasteReporting.API.Models;

namespace WasteReporting.API.Services;

public class CollectionService : ICollectionService
{
    private readonly AppDbContext _context;

    public CollectionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CollectionResponseDto> ScheduleCollectionAsync(CreateCollectionDto dto)
    {
        var collection = new Collection
        {
            CollectionPointId = dto.CollectionPointId,
            RecyclerId = dto.RecyclerId,
            FinalDestinationId = dto.FinalDestinationId,
            CollectionDate = dto.CollectionDate,
            Status = dto.Status
        };

        _context.Collections.Add(collection);
        await _context.SaveChangesAsync();

        return await GetCollectionByIdAsync(collection.Id);
    }

    public async Task<CollectionResponseDto> GetCollectionByIdAsync(int id)
    {
        var collection = await _context.Collections
            .Include(c => c.CollectionPoint)
            .Include(c => c.Recycler)
            .Include(c => c.FinalDestination)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (collection == null) throw new Exception("Collection not found");

        return MapToDto(collection);
    }

    public async Task<IEnumerable<CollectionResponseDto>> ListCollectionsAsync()
    {
        var collections = await _context.Collections
            .Include(c => c.CollectionPoint)
            .Include(c => c.Recycler)
            .Include(c => c.FinalDestination)
            .ToListAsync();

        return collections.Select(MapToDto);
    }

    public async Task<CollectionResponseDto> UpdateCollectionAsync(int id, UpdateCollectionDto dto)
    {
        var collection = await _context.Collections.FindAsync(id);
        if (collection == null) throw new Exception("Collection not found");

        collection.CollectionPointId = dto.CollectionPointId;
        collection.RecyclerId = dto.RecyclerId;
        collection.FinalDestinationId = dto.FinalDestinationId;
        collection.CollectionDate = dto.CollectionDate;
        collection.Status = dto.Status;

        await _context.SaveChangesAsync();
        return await GetCollectionByIdAsync(id);
    }

    public async Task DeleteCollectionAsync(int id)
    {
        var collection = await _context.Collections.FindAsync(id);
        if (collection == null) throw new Exception("Collection not found");

        _context.Collections.Remove(collection);
        await _context.SaveChangesAsync();
    }

    public async Task AssociateWasteAsync(CreateCollectionWasteDto dto)
    {
        var association = new CollectionWaste
        {
            CollectionId = dto.CollectionId,
            WasteId = dto.WasteId,
            WeightKg = dto.WeightKg
        };

        _context.CollectionWastes.Add(association);
        await _context.SaveChangesAsync();
    }

    public async Task DisassociateWasteAsync(int collectionId, int wasteId)
    {
        var association = await _context.CollectionWastes.FindAsync(collectionId, wasteId);
        if (association == null) throw new Exception("Association not found");

        _context.CollectionWastes.Remove(association);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CollectionWasteResponseDto>> ListAssociationsAsync()
    {
        var associations = await _context.CollectionWastes.ToListAsync();
        return associations.Select(a => new CollectionWasteResponseDto
        {
            CollectionId = a.CollectionId,
            WasteId = a.WasteId,
            WeightKg = a.WeightKg
        });
    }

    private static CollectionResponseDto MapToDto(Collection c)
    {
        return new CollectionResponseDto
        {
            Id = c.Id,
            CollectionPoint = c.CollectionPoint != null ? new CollectionPointDto { Id = c.CollectionPoint.Id, Location = c.CollectionPoint.Location } : null,
            Recycler = c.Recycler != null ? new RecyclerDto { Id = c.Recycler.Id, Name = c.Recycler.Name } : null,
            FinalDestination = c.FinalDestination != null ? new FinalDestinationDto { Id = c.FinalDestination.Id, Description = c.FinalDestination.Description } : null,
            CollectionDate = c.CollectionDate,
            Status = c.Status
        };
    }
}
