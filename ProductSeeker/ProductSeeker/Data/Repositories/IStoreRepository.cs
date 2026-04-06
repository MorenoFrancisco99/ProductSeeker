using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IStoreRepository 
{
    Task <StoreCoreModel?> GetCoreByID(int coreId);
    Task <StoreSpecModel?> GetSpecByID(int specId);
    Task<StoreCoreModel> CreateCore(StoreCoreModel storeCore);
    Task<StoreSpecModel> CreateSpec(StoreSpecModel storeSpec);
    Task<StoreCoreModel?> GetByName(string name);
    Task<bool> IsCoreOwner(int CoreId, string UserId);
    Task<bool> IsSpecOwner(int SpecId, string UserId);
}
