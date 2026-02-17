using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductSeeker.Data.DTOs;
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

        //GET: api/store
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<StoreCoreModel>>> GetAllStores()
        {
            
        }
    }
}
