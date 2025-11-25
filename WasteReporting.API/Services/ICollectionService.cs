using WasteReporting.API.DTOs;

namespace WasteReporting.API.Services;

public interface ICollectionService
{
    Task<CollectionResponseDto> ScheduleCollectionAsync(CreateCollectionDto dto);
    Task<CollectionResponseDto> GetCollectionByIdAsync(int id);
    Task<IEnumerable<CollectionResponseDto>> ListCollectionsAsync();
    Task<CollectionResponseDto> UpdateCollectionAsync(int id, UpdateCollectionDto dto);
    Task DeleteCollectionAsync(int id);

    Task AssociateWasteAsync(CreateCollectionWasteDto dto);
    Task DisassociateWasteAsync(int collectionId, int wasteId);
    Task<IEnumerable<CollectionWasteResponseDto>> ListAssociationsAsync();
}
