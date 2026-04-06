using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<AppUser?>> GetUserByID(string id)
        {
            return await _userManager.FindByIdAsync(id);
            
        }

        public Task<Result<string>> GetUserGeoLocation(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<string>>> GetUserRoles(string id)
        {
            return await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(id))
                .ContinueWith(task => (Result<IEnumerable<string>>)task.Result);
        }
    }
}