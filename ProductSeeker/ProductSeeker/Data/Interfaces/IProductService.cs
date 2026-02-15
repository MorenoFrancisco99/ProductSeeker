using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IProductService
{
    Task<IEnumerable<ProductCoreModel>> GetAllProducts();
    Task<ProductCoreModel?> GetByID(int id);
    Task<ProductCoreModel?> CreateNewProduct( ProductCoreModel productCore);

}
