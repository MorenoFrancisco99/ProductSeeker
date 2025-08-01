using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetProductsAsync();
        Task<ProductModel?> GetByIDAsync(int id);
        Task<ProductModel> PostProductAsync(ProductModel model);
        Task<ProductModel?> PutProductAsync(int id, PUTProductDTO productDTO);
        Task<List<ProductModel>> DeleteAllProductsAsync();
        Task<ProductModel?> DeleteByIDAsync(int id);

        /// <summary>
        /// Retrieves the full history of a product by its ID.
        /// </summary>
        /// <remarks>
        ///    
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<ProductHistoryDTO>?> GetAllProductHistoryAsync(int id);
    }
}
