using WasteReporting.API.DTOs;

namespace WasteReporting.API.Services;

public interface IManagementService
{
    Task<CollectionPointDto> CreateCollectionPointAsync(CreateCollectionPointDto dto);
    Task<RecyclerDto> CreateRecyclerAsync(CreateRecyclerDto dto);
    Task<FinalDestinationDto> CreateFinalDestinationAsync(CreateFinalDestinationDto dto);
    Task<WasteDto> CreateWasteAsync(CreateWasteDto dto);

    Task<IEnumerable<WasteDto>> ListWastesAsync(int page, int pageSize);
}
