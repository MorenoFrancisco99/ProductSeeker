using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Interfaces
{
    public interface IStoreService
    {
        public Task<List<StoreDTO>> GetAllStoresAsync();
        public Task<StoreModel?> CreateStoreAsync(AppUser user, PostStoreDTO userStore);
        public Task<List<StoreDTO>> GetUserStoresAsync(AppUser user);
    }
}
