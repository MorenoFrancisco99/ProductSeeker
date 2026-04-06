using ProductSeeker;
using ProductSeeker.Data.Models;

public interface IUserService
    {
        //Task<Result<NewUserDTO>> Register(RegisterDTO registerDTO);
        //Task<Result<NewUserDTO>> Login(LoginDTO loginDTO);
        Task<Result<AppUser?>> GetUserByID(string id);
        Task<Result<IEnumerable<string>>> GetUserRoles(string id);
        Task<Result<string?>> GetUserGeoLocation(string id);
    }