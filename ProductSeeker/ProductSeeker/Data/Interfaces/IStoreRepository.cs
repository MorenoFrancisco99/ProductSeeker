using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IStoreRepository 
{
    Task <StoreCoreModel?> GetCoreByID(int id);
    Task <StoreSpecModel?> GetSpecByID(int id);
    Task<StoreCoreModel?> CreateCore(StoreCoreModel storeCore);
    Task<StoreSpecModel?> CreateSpec(StoreSpecModel storeSpec);
    Task<StoreCoreModel?> GetStoreByName(string name);
    Task<bool> IsCoreOwner(int CoreId, string UserId);
    Task<bool> IsSpecOwner(int SpecId, string UserId);
}
