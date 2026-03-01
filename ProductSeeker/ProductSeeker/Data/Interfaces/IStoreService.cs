using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IStoreService
{
    Task<StoreCoreModel?> GetCoreByID(int id);
    Task<StoreSpecModel?> GetSpecByID(int id);
    Task<StoreCoreModel> GetByName(string name);
    Task<StoreCoreModel?> CreateStoreWSpec( StoreWSpecDTO storeDTO, string userID);
    Task<StoreCoreModel?> CreateStoreCore (StoreCoreDTO storeDTO, string userID);
    Task<StoreSpecModel?> CreateStoreSpec(StoreSpecDTO storeDTO, string userID);
   
    Task<bool> IsCoreOwner(int CoreId, string UserId);
    Task<bool> IsSpecOwner(int SpecId, string UserId);

}
