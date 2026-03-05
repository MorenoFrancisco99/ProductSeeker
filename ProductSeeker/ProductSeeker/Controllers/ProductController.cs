using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProductSeeker.Services.Extensions;
using Microsoft.AspNetCore.Identity;
using ProductSeeker.Data.Models;
using System.Security.Claims;

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

        //GET: api/product/core/id
        [HttpGet("core/{id}")]
        [Authorize]
        public async Task<ActionResult<ProductCoreModel>> GetCoreByID(int id)
        {
            try
            {
                var result = await _productService.GetCoreByID(id);
                if (result == null) { return NotFound(); }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching product core: ", ex);
                return StatusCode(500);
            }
        }

        //GET: api/product/spec/id
        [HttpGet("spec/{id}")]
        [Authorize]
        public async Task<ActionResult<ProductSpecModel>> GetSpecByID(int id)
        {
            try
            {
                var result = await _productService.GetSpecByID(id);
                if (result == null) { return NotFound(); }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching product core: ", ex);
                return StatusCode(500);
            }
        }

        //POST: api/product/core
        [HttpPost("core")]
        [Authorize]
        public async Task<ActionResult<ProductCoreModel>> POSTCore([FromBody] ProductCoreDTO productDTO)
        {
            string? userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userID == null) { return Unauthorized(); }
            try
            {
                var result = await _productService.CreateProductCore(productDTO, userID);

                return CreatedAtAction(nameof(GetCoreByID), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating product core: ", ex);
                return StatusCode(500);
            }
        }

        //POST: api/product/spec
        [HttpPost("spec")]
        [Authorize]
        public async Task<ActionResult<ProductSpecModel>> POSTSpec([FromBody] ProductSpecDTO dto)
        {
            if (!ModelState.IsValid){ return BadRequest(ModelState);}

            string? userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userID == null) { return Unauthorized(); }

            try
            {
                var result = await _productService.CreateProductSpec(dto, userID);

                return CreatedAtAction(nameof(GetSpecByID), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating product spec: ", ex);
                return StatusCode(500);
            }
        }

        //POST: api/product/

     
    }
}
