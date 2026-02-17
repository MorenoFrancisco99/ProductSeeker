using ProductSeeker.Data.Models;

namespace ProductSeeker;

public class StoreService : IStoreService
{
    public Task<StoreCoreModel?> CreateNewProduct(StoreCoreModel storeCore)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<StoreCoreModel>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public Task<StoreCoreModel?> GetByID(int id)
    {
        throw new NotImplementedException();
    }
}
