using WasteReporting.API.ViewModels;

namespace WasteReporting.API.Services;

public interface ICollectionService
{
    Task<CollectionResponseViewModel> ScheduleCollectionAsync(CreateCollectionViewModel dto);
    Task<CollectionResponseViewModel> GetCollectionByIdAsync(int id);
    Task<IEnumerable<CollectionResponseViewModel>> ListCollectionsAsync(int page, int pageSize);
    Task<CollectionResponseViewModel> UpdateCollectionAsync(int id, UpdateCollectionViewModel dto);
    Task DeleteCollectionAsync(int id);

    Task AssociateWasteAsync(CreateCollectionWasteViewModel dto);
    Task DisassociateWasteAsync(int collectionId, int wasteId);
    Task<IEnumerable<CollectionWasteResponseViewModel>> ListAssociationsAsync();
}
