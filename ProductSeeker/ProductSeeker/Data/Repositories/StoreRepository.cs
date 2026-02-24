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

    public async Task<StoreCoreModel?> GetCoreByID(int id)
    {
        var result = await _context.StoreCores.FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }

    public async Task<StoreCoreModel?> CreateCore(StoreCoreModel storeCore)
    {
         return await _storeCoreRepo.Create(storeCore);
    }

    public async Task<StoreCoreModel?> GetStoreByName(string name)
    {
        throw new NotImplementedException();
    }




    
}
