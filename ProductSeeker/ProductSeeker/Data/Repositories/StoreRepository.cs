using Microsoft.EntityFrameworkCore;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AplicationDBContext _context;
        public StoreRepository(AplicationDBContext context) 
        { 
            _context = context;
        }

        public async Task<List<StoreModel>> DeleteAllStoreAsync()
        {
            var storeList = await _context.Stores.ToListAsync();
            _context.Stores.RemoveRange(storeList);
            await _context.SaveChangesAsync();

            return storeList;

        }

        public async Task<StoreModel?> DeleteStoreByIDAsync(int id)
        {
            var storeModel = await _context.Stores.FirstOrDefaultAsync(x => x.Id == id);
            if (storeModel == null) { return null; }
            _context.Stores.Remove(storeModel);
            await _context.SaveChangesAsync();

            return storeModel;

        }

        public async Task<StoreModel?> GetStoreByIdAsync(int id)
        {
            return await _context.Stores.Include(p => p.ProductList).FirstOrDefaultAsync(x =>x.Id == id);

        }
        public async Task<List<StoreModel>> GetStoresAsync()
        {
            return await _context.Stores.Include(p => p.ProductList).ToListAsync();

        }

        public async Task<StoreModel> PostStoreAsync(StoreModel store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<bool> StoreExist(int id)
        {
            return await _context.Stores.AnyAsync(s => s.Id == id);
        }
    }
}
