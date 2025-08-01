using Microsoft.EntityFrameworkCore;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Repositories
{
    public class AppUserProductRepository : IAppUserProductRepository
    {
        private readonly AplicationDBContext _context;

        public AppUserProductRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ProductModel?> GetProductByIdAsync(AppUser user, int id)
        {
            var product = await _context.AppUserProducts
                .Where(up => up.AppUserId == user.Id && up.ProductId == id)
                .Select(up => up.ProductModel)
                .FirstOrDefaultAsync();
            return product;
        }

        public async Task<List<ProductModel>> GetUserProductsAsync(AppUser user)
        {
            var products = await _context.AppUserProducts
                .Where(up => up.AppUserId == user.Id)
                .Select(up => up.ProductModel)
                .ToListAsync();
            return products;
        }

        public async Task<AppUserProduct?> PostUserProductAsync(AppUser user, ProductModel model)
        {
            try
            {
                var appUserProduct = new AppUserProduct
                {
                    AppUserId = user.Id,
                    ProductId = model.Id
                };
                _context.AppUserProducts.Add(appUserProduct);
                await _context.SaveChangesAsync();
                return appUserProduct;
            }
            catch (Exception ex) { return null; }

        }
    }
}
