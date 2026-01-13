using ProductSeeker.Data.OldModels;

namespace ProductSeeker.Data.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
