using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IProductService
{
    Task<IEnumerable<ProductCoreModel>> GetAllProducts();
    Task<Result<ProductCoreModel>?> GetCoreByID(int CoreId, string userID);
    Task<Result<ProductSpecModel>?>  GetSpecByID(int SpecId, string userID);
    Task<Result<AppUserProductPriceModel>> GetPriceByID(int priceId, string userID);
    Task<Result<ProductCoreModel>?> CreateProductCore(ProductCoreDTO productDTO, string userID);
    Task<Result<ProductSpecModel>?> CreateProductSpec(ProductSpecDTO productDTO, string userID);
    Task<Result<AppUserProductPriceModel>?> CreateProductPrice(POSTProductPriceDTO dto, string userID);
    Task<bool> IsCoreOwner(int CoreId, string UserId);
    Task<bool> IsSpecOwner(int SpecId, string UserId);

}
