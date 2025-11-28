using WasteReporting.API.ViewModels;

namespace WasteReporting.API.Services;

public interface IManagementService
{
    Task<CollectionPointViewModel> CreateCollectionPointAsync(CreateCollectionPointViewModel dto);
    Task<RecyclerViewModel> CreateRecyclerAsync(CreateRecyclerViewModel dto);
    Task<FinalDestinationViewModel> CreateFinalDestinationAsync(CreateFinalDestinationViewModel dto);
    Task<WasteViewModel> CreateWasteAsync(CreateWasteViewModel dto);

    Task<IEnumerable<WasteViewModel>> ListWastesAsync(int page, int pageSize);
}
