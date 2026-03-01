using System.Collections;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Services.Mappers;

namespace ProductSeeker;

public class ProductService : IProductService 
{

    private readonly IProductRepository _productRepo;

    public ProductService(IProductRepository prodRepo)
    {
        _productRepo = prodRepo;
    }

    public async Task<ProductCoreModel?> CreateProductCore(ProductCoreDTO productDTO, string userID)
    {
        var model = productDTO.FromProductCoreDTOToModel(userID);
        return await _productRepo.CreateCore(model);
    }

    public Task<ProductSpecModel?> CreateProductSpec(ProductSpecDTO productDTO, string userID)
    {
        
    }

    public Task<IEnumerable<ProductCoreModel>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public async Task<ProductCoreModel?> GetCoreByID(int id)
    {
        return await _productRepo.GetCoreByID(id);
    }

    public async Task<ProductSpecModel?> GetSpecByID(int id)
    {
        return await _productRepo.GetSpecByID(id);
    }

    public Task<bool> IsCoreOwner(int CoreId, string UserId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsSpecOwner(int SpecId, string UserId)
    {
        throw new NotImplementedException();
    }
}
