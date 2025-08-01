using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Interfaces
{
    public interface IAppUserProductRepository
    {
        Task<List<ProductModel>> GetUserProductsAsync(AppUser user);

        Task<AppUserProduct?> PostUserProductAsync(AppUser user, ProductModel model);

        /// <summary>
        /// Checks if a product exists for a specific user by product ID and user ID.
        /// </summary>
        /// <remarks>
        /// Can be used to verify if a product exist and is associated with the user.
        /// </remarks>
        Task<ProductModel?> GetProductByIdAsync(AppUser user, int id);
    }
}
