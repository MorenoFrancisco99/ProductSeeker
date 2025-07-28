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

        public Task<ProductDTO> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<ProductDTO?> IProductService.CreateProductAsync(POSTProductDTO product, AppUser user)
        {
            throw new NotImplementedException();
        }

        Task<List<ProductDTO>> IProductService.GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        Task<ProductDTO> IProductService.GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDTO>> GetUserProductsAsync(AppUser user)
        {
            var userProducts = await  _appUserProductRepository.GetUserProductsAsync(user);
            var userProductsDTO =  userProducts.Select(p => p.toProductDTO()).ToList();
            return userProductsDTO;
        }


    }
}
