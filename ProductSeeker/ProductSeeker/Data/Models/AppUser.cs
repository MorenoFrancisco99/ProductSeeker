using Microsoft.AspNetCore.Identity;

namespace ProductSeeker.Data.Models
{
    public class AppUser : IdentityUser
    {
        //Se pueden agregar cosas custom para los user pero el IdentityUser parece agregar cosas basicas como password
        public List<AppUserStore> AppUserStores { get; set; } = new List<AppUserStore>();
        public List<AppUserProduct> AppUserProducts { get; set; } = new List<AppUserProduct>();
    }
}
