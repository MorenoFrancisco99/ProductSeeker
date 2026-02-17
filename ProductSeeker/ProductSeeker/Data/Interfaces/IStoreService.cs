using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IStoreService
{
    Task<IEnumerable<StoreCoreModel>> GetAllProducts();
    Task<StoreCoreModel?> GetByID(int id);
    Task<StoreCoreModel?> CreateNewProduct( StoreCoreModel storeCore);
}
