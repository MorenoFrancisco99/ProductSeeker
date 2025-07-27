using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Services.Extensions;
using ProductSeeker.Services.Mappers;

namespace ProductSeeker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppUserStoreController : ControllerBase
    {
        readonly UserManager<AppUser> _userManager;
        readonly IStoreRepository _storeRepository;
        readonly IAppUserStoreRepository _appUserStoreRepo;
        public AppUserStoreController(UserManager<AppUser> userManager, IStoreRepository storeRepository, IAppUserStoreRepository appUserStoreRepo)
        {
            _userManager = userManager;
            _storeRepository = storeRepository;
            _appUserStoreRepo = appUserStoreRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserStores()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) { return NotFound("User not found"); }
            var userStores = await _appUserStoreRepo.GetUserStoresAsync(appUser);
            return Ok(userStores);
        }



        }

        // Posible boilerplate para aññadir stores foraneos
        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> AddStoreToUser([FromBody] StoreModel store)
        //{
        //    if (store == null) return BadRequest("Store cannot be null");
        //    var username = User.GetUsername();
        //    var appUser = await _userManager.FindByNameAsync(username);
        //    if (appUser == null) return NotFound("User not found");
        //    // Assuming you have a method to add a store to the user
        //    var result = await _appUserStoreRepo.AddStoreToUserAsync(appUser, store);
        //    if (!result) return BadRequest("Failed to add store to user");
        //    return Ok("Store added successfully");
        //}
    }
