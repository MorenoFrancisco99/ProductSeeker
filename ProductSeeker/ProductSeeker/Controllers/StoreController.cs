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
                if (result.IsSuccess) { Ok(result.Value); }

                return result.Error.Type switch
                {
                    ErrorType.NotFound => NotFound(result.Error.Description),
                    ErrorType.Forbidden => Forbid(result.Error.Description)
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Store core by ID: {ex}");
                return StatusCode(500);
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
                if (result.IsSuccess) { Ok(result.Value); }

                return result.Error.Type switch
                {
                    ErrorType.NotFound => NotFound(result.Error.Description),
                    ErrorType.Forbidden => Forbid(result.Error.Description)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching store spec by ID", ex);
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


                //Cnnot return Error here.
                //For the sake of consistensy and future proofing its still gets managed
                return result.Error.Type switch
                {
                    ErrorType.NotFound => NotFound($"{result.Error.Description} + {result.Error.Metadata}"),
                    ErrorType.Forbidden => Forbid($"{result.Error.Description} + {result.Error.Metadata}")
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating store core: ", ex);
                return StatusCode(500);
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
            
                 return result.Error.Type switch
                {
                    ErrorType.NotFound => NotFound($"{result.Error.Description} + {result.Error.Metadata}"),
                    ErrorType.Forbidden => Forbid($"{result.Error.Description} + {result.Error.Metadata}"),
                    ErrorType.Validation => UnprocessableEntity($"{result.Error.Description} + {result.Error.Metadata}")
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating store spec:", ex);
                return StatusCode(500);
            }

        }
    }
}
