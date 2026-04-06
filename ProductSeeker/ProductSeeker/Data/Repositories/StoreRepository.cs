using Microsoft.EntityFrameworkCore;
using ProductSeeker.Controllers;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Data.Repositories;

namespace ProductSeeker;



public class StoreRepository : IStoreRepository
{
    private readonly AplicationDBContext _context;
    public StoreRepository(IGenericRepository<StoreCoreModel> storeCoreRepo,
                        IGenericRepository<StoreSpecModel> storeSpecRepo,
                        AplicationDBContext context)
    {
        _context = context;
    }

    public async Task<StoreCoreModel?> GetCoreByID(int coreId)
    {
        return await _context.StoreCores.FirstOrDefaultAsync(x => x.Id == coreId);
       
    }

    public async Task<StoreSpecModel?> GetSpecByID(int specId)
    {
        return await _context.StoreSpecs.FirstOrDefaultAsync(x => x.Id == specId);
    }

    public async Task<StoreCoreModel> CreateCore(StoreCoreModel storeCore)
    {
        _context.StoreCores.Add(storeCore);
        await _context.SaveChangesAsync();
        return storeCore;
    }
    public async Task<StoreSpecModel> CreateSpec(StoreSpecModel storeSpec)
    {
        _context.StoreSpecs.Add(storeSpec);
        await _context.SaveChangesAsync();
        return storeSpec;
    }

    public async Task<StoreCoreModel?> GetByName(string name)
    {
        return await _context.StoreCores.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<bool> IsCoreOwner(int CoreId, string UserId)
    {
        var result = await _context.StoreCores
        .FirstOrDefaultAsync(x => x.Id == CoreId && x.IdCreator == UserId);

        if (result == null) { return false; }
        return true;
    }

    public Task<bool> IsSpecOwner(int SpecId, string UserId)
    {
        throw new NotImplementedException();
    }

   
}
