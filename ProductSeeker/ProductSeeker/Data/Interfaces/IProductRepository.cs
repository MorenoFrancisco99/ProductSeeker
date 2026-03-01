using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IProductRepository
{
    Task<ProductCoreModel?> GetCoreByID(int id);
    Task<ProductSpecModel?> GetSpecByID(int id);

    Task<ProductCoreModel?> CreateCore(ProductCoreModel productCore);
    Task<ProductSpecModel?> CreateSpec(ProductSpecModel productSpec);
    Task<bool> IsCoreOwner(int CoreId, string UserId);
    Task<bool> IsSpecOwner(int SpecId, string UserId);

    Task<bool> CoreExist(int coreID);
    Task<bool> SpecExist(int specID);


}
