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
        public async Task<List<ProductModel>> GetUserProductsAsync(AppUser user)
        {
            throw new NotImplementedException("This method is not implemented yet. Please implement it in the future.");
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
