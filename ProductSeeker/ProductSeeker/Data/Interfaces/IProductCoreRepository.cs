using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Interfaces;

public interface IProductCoreRepository
{
    public Task<ProductCoreModel> CreateProductCore(ProductCoreModel productCore);
}