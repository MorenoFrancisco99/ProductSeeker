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

        // POST: api/product
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] POSTProductDTO productDto)
        {
            /*
             TODO: The asociated store comes in the DTO in form of an ID. 
             Later change to a full StoreDTO or param(not ID) to associate the correct store.
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

    }


}
