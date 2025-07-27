using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Interfaces
{
    public interface IAppUserStoreRepository
    {
        Task<List<StoreModel>> GetUserStoresAsync(AppUser user);
        Task<AppUserStore> PostUserStoreAsync(AppUser user, StoreModel store);
        /// <summary>
        /// Searches for a user store by storeId, and user if you want.
        /// </summary>
        /// <remarks>
        ///     User can be null, in which case it will search only by storeId.
        /// </remarks>
        Task<AppUserStore?> GetUserStoreByStoreId(AppUser? user, int storeId);
    }
}
