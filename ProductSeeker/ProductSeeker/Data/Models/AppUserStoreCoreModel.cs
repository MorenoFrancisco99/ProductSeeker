using System.ComponentModel.DataAnnotations;

namespace ProductSeeker.Data.Models;

/// <summary>
/// Join table of user and a StoreCore.
/// Stores Metadata and user related info of the relationship
/// </summary>
public class AppUserStoreCoreModel : BaseEntity
{
    [Required]
    public required int StoreId { get; set; }
    [Required]
    public required StoreCoreModel StoreCore { get; set; }
    
    [Required]
    public bool? UserRating { get; set; }
}