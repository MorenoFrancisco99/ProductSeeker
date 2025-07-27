using Microsoft.EntityFrameworkCore;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Repositories
{
    public class AppUserStoreRepository : IAppUserStoreRepository
    {
        private readonly AplicationDBContext _context;
        public AppUserStoreRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<AppUserStore> PostUserStoreAsync(AppUser user, StoreModel store)
        {
            if (user == null || store == null)
            {
                throw new ArgumentNullException("User or store cannot be null");
            }
            var appUserStore = new AppUserStore
            {
                AppUserId = user.Id,
                StoreId = store.Id
            };
            _context.AppUserStores.Add(appUserStore);
            await _context.SaveChangesAsync();
            return appUserStore;
        }

        public async Task<List<StoreModel>> GetUserStoresAsync(AppUser user)
        {

            var stores = await _context.AppUserStores
                .Include(x => x.StoreModel.ProductList)
                .Where(x => x.AppUserId == user.Id)
                .Select(x => x.StoreModel)
                .ToListAsync();
            return stores!;
        }

        public async Task<AppUserStore?> GetUserStoreByStoreId(AppUser? user, int storeId)
        {
            if (user == null)
            {
                //search only by storeId
                return await _context.AppUserStores
                    .FirstOrDefaultAsync(x => x.StoreId == storeId);
            }
            return await _context.AppUserStores
                .FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.StoreId == storeId);
        }


    }
}
