using System.Collections;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Services.Mappers;

namespace ProductSeeker;

public class ProductService : IProductService
{

    private readonly IProductRepository _productRepo;
    private readonly IStoreRepository _storeRepo;

    public ProductService(IProductRepository prodRepo, IStoreRepository storeRepo)
    {
        _productRepo = prodRepo;
        _storeRepo = storeRepo;
    }

    public async Task<Result<ProductCoreModel>?> CreateProductCore(ProductCoreDTO productDTO, string userID)
    {
        var model = productDTO.FromProductCoreDTOToModel(userID);
        return await _productRepo.CreateCore(model);
    }

    public async Task<Result<AppUserProductPriceModel>?> CreateProductPrice(POSTProductPriceDTO dto, string userID)
    {
        var specRes = await _productRepo.GetSpecByID(dto.ProductSpecId, userID);
        if (!specRes.IsSuccess) { return specRes.Error!; }

        var storeRes = await _storeRepo.GetCoreByID(dto.StoreId, userID);
        if (!storeRes.IsSuccess) { return storeRes.Error!; }

        var model = dto.MapToModel(userID);
        return await _productRepo.CreatePrice(model);

    }

    public async Task<Result<ProductSpecModel>?> CreateProductSpec(ProductSpecDTO dto, string userID)
    {
        var res = await _productRepo.GetCoreByID(dto.ProductCoreId, userID);
        if(! res.IsSuccess) { return res.Error! ;}
        
        var model = dto.ToModel(userID);

        return await _productRepo.CreateSpec(model);

    }

    public Task<IEnumerable<ProductCoreModel>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ProductCoreModel>?> GetCoreByID(int CoreId, string userID)
    {
        return await _productRepo.GetCoreByID(CoreId, userID);
    }

    public async Task<Result<AppUserProductPriceModel>> GetPriceByID(int priceId, string userID)
    {
        return await _productRepo.GetPriceByID(priceId, userID);
    }

    public async Task<Result<ProductSpecModel>?> GetSpecByID(int SpecId, string userID)
    {
        return await _productRepo.GetSpecByID(SpecId, userID);
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
