using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Services.Extensions;

namespace ProductSeeker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly UserManager<AppUser> _userManager;

        public StoreController(IStoreService storeService, UserManager<AppUser> userManager)
        {
            _storeService = storeService;
            _userManager = userManager;
        }

        // GET: api/store
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<StoreDTO>>> GetAllStores()
        {
            try
            {
                var stores = await _storeService.GetAllStoresAsync();
                return Ok(stores);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, $"Internal server error{ex}");
            }
        }

        // POST: api/store
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<StoreModel>> CreateStore([FromBody] PostStoreDTO userStore)
        {
            if (userStore == null)
            {
                return BadRequest("Store data is null");
            }
            try
            {
                var username = User.GetUsername();
                var user = await _userManager.FindByNameAsync(username);
                if (user == null) { return NotFound("User not found"); }
                var result = await _storeService.CreateStoreAsync(user, userStore);
                if (result == null)
                {
                    return BadRequest("Store creation failed");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

        // GET: api/store/user-stores
        [HttpGet("user-stores")]
        [Authorize]
        public async Task<ActionResult<List<StoreModel>>> GetUserStores()
        {
            try
            {
                var username = User.GetUsername();
                var user = await _userManager.FindByNameAsync(username);
                if (user == null) { return NotFound("User not found"); }
                var userStores = await _storeService.GetUserStoresAsync(user);
                return Ok(userStores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }


    }
}
