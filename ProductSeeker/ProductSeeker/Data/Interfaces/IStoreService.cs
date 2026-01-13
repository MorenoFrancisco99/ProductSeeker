using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.OldModels;

namespace ProductSeeker.Data.Interfaces
{
    public interface IStoreService
    {
        public Task<List<StoreDTO>> GetAllStoresAsync();
        public Task<StoreDTO?> CreateStoreAsync(AppUser user, PostStoreDTO userStore);
        public Task<List<StoreDTO>> GetUserStoresAsync(AppUser user);
    }
}
