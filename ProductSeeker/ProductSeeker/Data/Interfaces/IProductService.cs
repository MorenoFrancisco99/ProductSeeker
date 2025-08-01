using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Gets the product by its ID.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>The product with the specified ID.</returns>
        Task<ProductDTO?> GetProductByIdAsync(AppUser user, int id);
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        Task<List<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> CreateProductAsync(POSTProductDTO product, AppUser user);
        Task<List<ProductDTO>> GetUserProductsAsync(AppUser user);

        Task<ProductDTO?> UpdateProductAsync(int id, PUTProductDTO product, AppUser user);


        Task<List<ProductHistoryDTO?>?> GetProductHistoryAsync(AppUser user, int id);
    }
}
