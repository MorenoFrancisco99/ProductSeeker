using Microsoft.EntityFrameworkCore;
using ProductSeeker.Controllers;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Data.Repositories;

namespace ProductSeeker;

/// <summary>
/// Repositories for store domain. Manages both Cores and Specs
/// </summary>

public class StoreRepository : IStoreRepository
{
    private readonly IGenericRepository<StoreCoreModel> _storeCoreRepo;
    private readonly IGenericRepository<StoreSpecModel> _storeSpecRepo;
    private readonly AplicationDBContext _context;
    public StoreRepository(IGenericRepository<StoreCoreModel> storeCoreRepo,
                        IGenericRepository<StoreSpecModel> storeSpecRepo,
                        AplicationDBContext context)
    {
        _storeCoreRepo = storeCoreRepo;
        _storeSpecRepo = storeSpecRepo;
        _context = context;
    }

    public async Task<Result<StoreCoreModel>?> GetCoreByID(int CoreId, string userID)
    {
        var result = await _context.StoreCores.FirstOrDefaultAsync(x => x.Id == CoreId);
        return result == null
        ? Errors.StoreCoreNotFound
        : result.IdCreator != userID
            ? Errors.NotOwner
            : result;
    }

    public async Task<Result<StoreSpecModel>?> GetSpecByID(int SpecId, string userID)
    {
        var result = await _context.StoreSpecs.FirstOrDefaultAsync(x => x.Id == SpecId);
        return result == null
        ? Errors.StoreSpecNotFound
        : result.IdCreator != userID
            ? Errors.NotOwner
            : result;
    }

    public async Task<Result<StoreCoreModel>> CreateCore(StoreCoreModel storeCore)
    {
        _context.StoreCores.Add(storeCore);
        await _context.SaveChangesAsync();
        return storeCore;
    }
    public async Task<Result<StoreSpecModel>> CreateSpec(StoreSpecModel storeSpec)
    {
        _context.StoreSpecs.Add(storeSpec);
        await _context.SaveChangesAsync();
        return storeSpec;
    }

    public async Task<StoreCoreModel?> GetStoreByName(string name)
    {
        throw new NotImplementedException();
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
