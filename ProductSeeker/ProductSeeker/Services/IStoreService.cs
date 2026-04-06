using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IStoreService
{
    Task<Result<StoreCoreModel>> GetCoreByID(int CoreId, string userID);
    Task<Result<StoreSpecModel>> GetSpecByID(int SpecId, string userID);
    Task<StoreCoreModel> GetByName(string name);
    Task<Result<StoreCoreModel>> CreateStoreWSpec( StoreWSpecDTO storeDTO, string userID);
    Task<Result<StoreCoreModel>> CreateStoreCore (StoreCoreDTO storeDTO, string userID);
    Task<Result<StoreSpecModel>?> CreateStoreSpec(StoreSpecDTO storeDTO, string userID);

    Task<bool> IsCoreOwner(int CoreId, string UserId);
    Task<bool> IsSpecOwner(int SpecId, string UserId);

}
