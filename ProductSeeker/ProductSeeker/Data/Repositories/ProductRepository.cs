using Microsoft.EntityFrameworkCore;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

public class ProductRepository : IProductRepository
{

    private readonly AplicationDBContext _context;

    public ProductRepository(AplicationDBContext context)
    {
        _context = context;
    }

    public async Task<bool> CoreExist(int coreID)
    {
        var result = _context.ProductCores.FirstOrDefaultAsync(x => x.Id == coreID);
        if(result == null) {return false;}
        return true;
    }

    public async Task<ProductCoreModel?> CreateCore(ProductCoreModel productCore)
    {
        _context.Add(productCore);
        await _context.SaveChangesAsync();
        return productCore;
    }

    public async Task<ProductSpecModel?> CreateSpec(ProductSpecModel productSpec)
    {
        _context.Add(productSpec);
        await _context.SaveChangesAsync();
        return productSpec;
    }

    public async Task<ProductCoreModel?> GetCoreByID(int id)
    {
        return await _context.ProductCores.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ProductSpecModel?> GetSpecByID(int id)
    {
        return await _context.ProductSpecs.FirstOrDefaultAsync(x => x.Id == id);
        
    }

    public async Task<bool> IsCoreOwner(int CoreId, string UserId)
    {
        var result = await _context.ProductCores.FirstOrDefaultAsync(p => p.Id == CoreId && p.IdCreator == UserId);
        if(result == null) { return false;}
        return true;
    }

    public async Task<bool> IsSpecOwner(int SpecId, string UserId)
    {
        var result = await _context.ProductSpecs.FirstOrDefaultAsync(p => p.Id == SpecId && p.IdCreator == UserId);
        if(result == null) { return false;}
        return true;
    }

    public Task<bool> SpecExist(int specID)
    {
        throw new NotImplementedException();
    }
}
