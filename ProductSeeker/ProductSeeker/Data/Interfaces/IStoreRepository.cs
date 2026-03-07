using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IStoreRepository 
{
    Task <Result<StoreCoreModel>?> GetCoreByID(int CoreId, string userID);
    Task <Result<StoreSpecModel>?> GetSpecByID(int SpecId, string userID);
    Task<Result<StoreCoreModel>> CreateCore(StoreCoreModel storeCore);
    Task<Result<StoreSpecModel>> CreateSpec(StoreSpecModel storeSpec);
    Task<StoreCoreModel?> GetStoreByName(string name);
    Task<bool> IsCoreOwner(int CoreId, string UserId);
    Task<bool> IsSpecOwner(int SpecId, string UserId);
}
