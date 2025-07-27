using System.ComponentModel.DataAnnotations;

namespace ProductSeeker.Data.DTOs.Account
{
    /// <summary>
    ///     Exactly as the name says
    /// </summary>
    public class RegisterDTO
    {
        /// <summary>
        /// Username. Example: "john_doe"
        /// </summary>
        [Required] public string? Username { get; set; }
        /// <summary>
        /// Password. Example: "P@ssw0rd123"
        /// </summary>
        [Required] public string? Password { get; set;}

        /// <summary>
        /// Email. Example: SomeEmail@SomeEmail.com
        /// </summary>
        [Required] [EmailAddress] public string? Email { get; set;}
    }
}
