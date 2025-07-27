using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Interfaces
{
    public interface IAppUserProductRepository
    {
        Task<List<ProductModel>> GetUserProductsAsync(AppUser user);

        Task<AppUserProduct> PostUserProductAsync(AppUser user, ProductModel model);
    }
}
