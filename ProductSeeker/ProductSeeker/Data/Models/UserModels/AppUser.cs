using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProductSeeker.Data.Models
{
    public class AppUser : IdentityUser

    {
    //Se pueden agregar cosas custom para los user pero el IdentityUser parece agregar cosas basicas como password
    // Para ver la lista completa de propiedades, visitar: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.identityuser?view=aspnetcore-10.0&viewFallbackFrom=net-10.0
    public required string? GeoLocation { get; set; } 
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreationDate { get; set; }


    }
}
