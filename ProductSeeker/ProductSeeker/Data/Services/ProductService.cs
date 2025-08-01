using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Services.Mappers;

namespace ProductSeeker.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IAppUserStoreRepository _appUserStoreRepository;
        private readonly IAppUserProductRepository _appUserProductRepository;
        public ProductService(
            IProductRepository productRepository,
            IStoreRepository storeRepository,
            IAppUserStoreRepository appUserStoreRepository,
            IAppUserProductRepository appUserProductRepository)
        {
            _productRepository = productRepository;
            _storeRepository = storeRepository;
            _appUserStoreRepository = appUserStoreRepository;
            _appUserProductRepository = appUserProductRepository;
        }




        public async Task<ProductDTO?> CreateProductAsync(POSTProductDTO product, AppUser user)
        {
            try
            {
                //check if the store exist and if the user is asociated with the store
                var userStore = await _appUserStoreRepository.GetUserStoreByStoreId(user, product.StoreId);
                if (userStore == null) return null;

                var productModel = product.fromPostDTOtoModel();
                var createdProduct = await _productRepository.PostProductAsync(productModel);
                if (createdProduct == null) return null;

                // Associate the product with the user
                var appUserProduct = await _appUserProductRepository.PostUserProductAsync(user, createdProduct);
                if (appUserProduct == null) return null;

                return productModel.toProductDTO();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return null;
            }
        }




        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            var productDTOs = products.Select(products => products.toProductDTO()).ToList();
            return productDTOs;
        }
     
  


        public async Task<ProductDTO?> GetProductByIdAsync(AppUser user, int id)
        {
           var product = await _appUserProductRepository.GetProductByIdAsync(user, id);
    
           return product?.toProductDTO();

        }




        public async Task<List<ProductDTO>> GetUserProductsAsync(AppUser user)
        {
            var userProducts = await  _appUserProductRepository.GetUserProductsAsync(user);
            var userProductsDTO =  userProducts.Select(p => p.toProductDTO()).ToList();
            return userProductsDTO;
        }




        public async Task<ProductDTO?> UpdateProductAsync(int id, PUTProductDTO product, AppUser user)
        {
            try
            {
                var productExisting = await _appUserProductRepository.GetProductByIdAsync(user, id);
                if (productExisting == null)
                {
                    return null;
                }
                var productModel = await _productRepository.PutProductAsync(id, product);
                return productModel?.toProductDTO();


            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return null;
            }

        }




        public async Task<List<ProductHistoryDTO?>?> GetProductHistoryAsync(AppUser user, int id)
        {
            try
            {
                var productExists = await _appUserProductRepository.GetProductByIdAsync(user, id);
                if (productExists == null)
                {
                    return null;
                }
                var productHistory = await _productRepository.GetAllProductHistoryAsync(id);
                return productHistory;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return null;
            }
        }
    }
}
