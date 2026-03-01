using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IProductService
{
    Task<IEnumerable<ProductCoreModel>> GetAllProducts();
    Task<ProductCoreModel?> GetCoreByID(int id);
    Task<ProductSpecModel?>  GetSpecByID(int id);
    
    Task<ProductCoreModel?> CreateProductCore(ProductCoreDTO productDTO, string userID);
    Task<ProductSpecModel?> CreateProductSpec(ProductSpecDTO productDTO, string userID);
    Task<bool> IsCoreOwner(int CoreId, string UserId);
    Task<bool> IsSpecOwner(int SpecId, string UserId);

}
