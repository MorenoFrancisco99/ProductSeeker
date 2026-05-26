using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Operation.Buffer;
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

        //GET: api/store/core/id
        [HttpGet("core/{id}")]
        [Authorize]
        public async Task<ActionResult<StoreCoreModel>> GetCoreByID(int id)
        {
            string? userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userID == null) { return Unauthorized(); }

            try
            {
                var result = await _storeService.GetCoreByID(id, userID);
                if (result.IsSuccess) { return Ok(result.Value); }

                return result.Error!.ToActionResult();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Store core by ID: {ex}");
                return StatusCode(500, "Internal server error.");
            }

        }

        //GET: api/store/spec/id
        [HttpGet("spec/{id}")]
        [Authorize]
        public async Task<ActionResult<StoreSpecModel?>> GetSpecByID(int id)
        {
            string? userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userID == null) { return Unauthorized(); }
            try
            {
                var result = await _storeService.GetSpecByID(id, userID);
                if (result.IsSuccess) { return Ok(result.Value); }

                return result.Error!.ToActionResult();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching store spec by ID", ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        //POST: api/store
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<StoreCoreModel>> POSTStoreWSPec([FromBody] StoreWSpecDTO storeDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userID == null) { return Unauthorized(); }

            try
            {
                var result = await _storeService.CreateStoreWSpec(storeDTO, userID);
                if (result.IsSuccess) { return CreatedAtAction(nameof(GetCoreByID), new { id = result.Value.Id }, result.Value); }

                return result.Error!.ToActionResult();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating store with spec: ", ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        //POST: api/store/core
        [HttpPost("core")]
        [Authorize]
        public async Task<ActionResult<StoreCoreModel>> POSTCore([FromBody] StoreCoreDTO storeDTO)
        {

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            //tomado de: https://stackoverflow.com/questions/46112258/how-do-i-get-current-user-in-net-core-web-api-from-jwt-token
            string? userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userID == null) { return Unauthorized(); }

            try
            {
                var result = await _storeService.CreateStoreCore(storeDTO, userID);

                if (result.IsSuccess)
                    return CreatedAtAction(nameof(GetSpecByID), new { id = result.Value.Id }, result.Value);
                return result.Error!.ToActionResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating store core: ", ex);
                return StatusCode(500, "Internal server error.");
            }
        }
        //GET: api/store/spec
        [HttpPost("spec")]
        [Authorize]
        public async Task<ActionResult<StoreSpecModel>> POSTSpec([FromBody] StoreSpecDTO storeDTO)
        {
            //Meant only for own stores
            //POSTing on foreign storecores requires special validations
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string? userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userID == null)
                return Unauthorized();

            try
            {
                var result = await _storeService.CreateStoreSpec(storeDTO, userID);
                if (result.IsSuccess)
                    return CreatedAtAction(nameof(GetSpecByID), new { id = result.Value.Id }, result.Value.Id);

                return result.Error!.ToActionResult();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating store spec:", ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
