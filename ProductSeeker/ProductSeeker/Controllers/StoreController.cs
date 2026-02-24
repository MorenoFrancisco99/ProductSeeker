using System.Security.Claims;
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
        public async Task<ActionResult<List<StoreCoreModel>>> GETAllStores()
        {
            throw new NotImplementedException();
        }

        [HttpGet("core/{id}")]
        [Authorize]
        public async Task<ActionResult<StoreCoreModel>> GetCoreByID(int id)
        {
            
            try
            {
                var result = await _storeService.GetCoreByID(id);

                if (result == null) {return NotFound();}

                return Ok(result);
                
            } 
            catch(Exception ex)
            {
                Console.WriteLine($"Error fetching Store core by ID: {ex}");
                return StatusCode(500);
            }
                
        }

        //POST: api/store
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> POSTStoreWSPec([FromBody] StoreWSpecDTO storeDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }


            return null;
        }

        //POST: api/store/core
        [HttpPost("core")]
        [Authorize]
        public async Task<ActionResult<StoreCoreModel>> POSTStoreCore([FromBody] StoreCoreDTO storeDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            //tomado de: https://stackoverflow.com/questions/46112258/how-do-i-get-current-user-in-net-core-web-api-from-jwt-token
            string? userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userID == null) {return Unauthorized();}
            
            var result = await _storeService.CreateStoreCore(storeDTO, userID);
            if (result == null) { return StatusCode(500); }

            return CreatedAtAction(nameof(GetCoreByID), new {id = result.Id}, result);

        }
    }
}
