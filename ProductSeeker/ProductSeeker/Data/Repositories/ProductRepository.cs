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
        if (result == null) { return false; }
        return true;
    }

    public async Task<ProductCoreModel?> CreateCore(ProductCoreModel productCore)
    {
        _context.ProductCores.Add(productCore);
        await _context.SaveChangesAsync();
        return productCore;
    }

    public async Task<AppUserProductPriceModel> CreatePrice(AppUserProductPriceModel model)
    {
        _context.AppUserProductPrices.Add(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<ProductSpecModel?> CreateSpec(ProductSpecModel productSpec)
    {
        _context.ProductSpecs.Add(productSpec);
        await _context.SaveChangesAsync();
        return productSpec;
    }

    public async Task<Result<ProductCoreModel>?> GetCoreByID(int CoreId, string userID)
    {
        var result = await _context.ProductCores.FirstOrDefaultAsync(x => x.Id == CoreId);

        return result == null
                    ? Errors.ProductCoreNotFound
                    : result.IdCreator != userID
                        ? Errors.NotOwner 
                        : result;

    }

    public async Task<Result<AppUserProductPriceModel>> GetPriceByID(int priceID, string userID)
    {
        var result = await _context.AppUserProductPrices.FirstOrDefaultAsync(x => x.Id == priceID);
        return result == null
                    ? Errors.ProductSpecNotFound
                    : result.IdCreator != userID
                        ? Errors.NotOwner
                        : result;
    }

    public async Task<Result<ProductSpecModel>?> GetSpecByID(int SpecId, string userID)
    {

        var result = await _context.ProductSpecs.FirstOrDefaultAsync(x => x.Id == SpecId);
        return result == null
                    ? Errors.ProductSpecNotFound
                    : result.IdCreator != userID
                        ? Errors.NotOwner
                        : result;

    }

    public async Task<Result> IsCoreOwner(int CoreId, string UserId)
    {
        var result = await _context.ProductCores.FirstOrDefaultAsync(p => p.Id == CoreId);
        if (result == null)
        {
            return Errors.ProductCoreNotFound;
        }
        else if (!(result.IdCreator == UserId))
        {
            return Errors.NotOwner;
        }
        return Result.Success();
    }

    public async Task<bool> IsSpecOwner(int SpecId, string UserId)
    {
        var result = await _context.ProductSpecs.FirstOrDefaultAsync(p => p.Id == SpecId && p.IdCreator == UserId);
        if (result == null) { return false; }
        return true;
    }

    public Task<bool> SpecExist(int specID)
    {
        throw new NotImplementedException();
    }
}
