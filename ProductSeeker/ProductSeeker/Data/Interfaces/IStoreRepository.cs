using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IStoreRepository 
{
    Task <StoreCoreModel?> GetCoreByID(int id);
    Task<StoreCoreModel?> CreateCore(StoreCoreModel storeCore);
    Task<StoreCoreModel?> GetStoreByName(string name);
}
