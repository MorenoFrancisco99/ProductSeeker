using Microsoft.AspNetCore.Mvc;
using ProductSeeker.Services.Mappers;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using ProductSeeker.Services.Extensions;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Identity;

namespace ProductSeeker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly UserManager<AppUser> _userManager;

        public ProductController(IProductService productService, UserManager<AppUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }


        // GET: api/product
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
          
            return Ok(products);
        }

        // GET: api/product/user-products
        [Authorize]
        [HttpGet("user-products")]
        public async Task<ActionResult<List<ProductDTO>>> GetUserProducts()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var products = await _productService.GetUserProductsAsync(user);
            return Ok(products);
        }




        // POST: api/product
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] POSTProductDTO productDto)
        {
            /*
             TODO: The asociated store comes in the DTO in form of an ID. 
             Later change to a full StoreDTO or another param(maybe standalone ID) to associate the correct store.
             Or maybe not. IDK im not your mother
             */
            if (productDto == null)
            {
                return BadRequest("Product data is null");
            }
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var createdProduct = await _productService.CreateProductAsync(productDto, user);
            
            if (createdProduct == null)
            {
                return BadRequest("Product creation failed");
            }
            return Ok(createdProduct);
        }

        // GET: api/product/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var product = await _productService.GetProductByIdAsync(user, id); 
            if (product == null)
            {
                return NotFound("Product does not exist or it doesn't belong to the user");
            }
            return Ok(product);

        }

        //PUT : api/product/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(int id, [FromBody] PUTProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is null");
            }
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var updatedProduct = await _productService.UpdateProductAsync(id, productDto, user);
            if (updatedProduct == null)
            {
                return NotFound("Product not found or update failed");
            }
            return Ok(updatedProduct);
        }

        // GET: api/product/product-history/{id}
        [Authorize]
        [HttpGet("product-history/{id}")]
        public async Task<ActionResult<List<ProductHistoryDTO>>> GetProductHistory(int id)
        {
            try
            {
                var username = User.GetUsername();
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                var productHistory = await _productService.GetProductHistoryAsync(user, id);
                return Ok(productHistory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }


}
