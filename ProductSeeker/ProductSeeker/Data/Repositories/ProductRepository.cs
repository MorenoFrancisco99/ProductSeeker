using Microsoft.EntityFrameworkCore;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AplicationDBContext _context;
        public ProductRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProductModel>> DeleteAllProductsAsync()
        {
            var allProducts = await _context.Products.ToListAsync();
            
            _context.Products.RemoveRange(allProducts);

            await _context.SaveChangesAsync();

            return allProducts;
        }

        public async Task<ProductModel?> DeleteByIDAsync(int id)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (prod == null) { return null; }

            _context.Products.Remove(prod);
            await _context.SaveChangesAsync();

            return prod;

        }

        public async Task<List<ProductModel>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ProductModel?> GetByIDAsync(int id)
        {
            return await _context.Products.FindAsync(id);

        }

        public async Task<ProductModel> PostProductAsync(ProductModel model)
        {
            _context.Products.Add(model);

            await _context.SaveChangesAsync();

            return model;

        }

        public async Task<ProductModel?> PutProductAsync(int id, PUTProductDTO productDTO)
        {
            throw
                new NotImplementedException("This method is not implemented yet. Please implement it in the future.");
        }
    }
}
