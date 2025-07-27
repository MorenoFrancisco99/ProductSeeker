using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Interfaces
{
    public interface IStoreRepository
    {
        Task<List<StoreModel>> GetStoresAsync();
        Task<StoreModel?> GetStoreByIdAsync(int id);
        Task<StoreModel> PostStoreAsync(StoreModel store);
        Task<List<StoreModel>> DeleteAllStoreAsync();
        Task<StoreModel?> DeleteStoreByIDAsync(int id);

        Task<bool> StoreExist(int id);
    }
}
