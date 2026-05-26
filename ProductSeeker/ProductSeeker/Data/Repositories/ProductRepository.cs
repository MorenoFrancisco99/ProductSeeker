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

    public async Task<ProductCoreModel?> FindCore(string productName, string brand)
    {
        return await _context.ProductCores
        .FirstOrDefaultAsync(x =>
            x.ProductName.ToLower() == productName.ToLower() &&
            x.Brand.ToLower() == brand.ToLower());
    }

    public async Task<ProductSpecModel?> FindSpecByIdentifiers(int coreId, List<object> specIdentifier)
    {   
        var specs = await _context.ProductSpecs.Where(x => x.ProductCoreId == coreId).ToListAsync();


        foreach (var spec in specs)
        {
            if (spec.GetIdentifier().SequenceEqual(specIdentifier))
            {
                return spec;
            }
        }

        return null;

    }

 

    public async Task<ProductCoreModel?> GetCoreByID(int CoreId)
    {
        return await _context.ProductCores.FirstOrDefaultAsync(x => x.Id == CoreId);
    }

    public async Task<AppUserProductPriceModel?> GetPriceByID(int priceID)
    {
        return await _context.AppUserProductPrices.FirstOrDefaultAsync(x => x.Id == priceID);

    }

    public async Task<ProductSpecModel?> GetSpecByEAN(string? ean)
    {
        if (ean == null)
            return null;
        return await _context.ProductSpecs.FirstOrDefaultAsync(x => x.EAN == ean);
    }

    public async Task<ProductSpecModel?> GetSpecByID(int SpecId)
    {
        return await _context.ProductSpecs.FirstOrDefaultAsync(x => x.Id == SpecId);
    }

}
