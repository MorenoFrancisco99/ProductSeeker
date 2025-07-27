using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
