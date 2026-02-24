using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IStoreService
{
    Task<IEnumerable<StoreCoreModel>> GetAllProducts();
    Task<StoreCoreModel?> GetCoreByID(int id);
    Task<StoreCoreModel> GetByName(string name);
    Task<StoreCoreModel?> CreateStoreWSpec( StoreWSpecDTO storeDTO);
    Task<StoreCoreModel?> CreateStoreCore (StoreCoreDTO storeDTO, string userID);
    Task<StoreCoreModel?> CreateStoreSpec(StoreSpecDTO storeDTO);

}
