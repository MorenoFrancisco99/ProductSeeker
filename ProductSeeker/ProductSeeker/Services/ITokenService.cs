using ProductSeeker.Data.Models;
namespace ProductSeeker.Data.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
