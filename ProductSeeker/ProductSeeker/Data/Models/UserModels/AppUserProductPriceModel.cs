using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductSeeker.Data.Models;


/// <summary>
/// Join table of user and a praticular economic unit.
/// Meant to store metadata and user data of the relationship
/// </summary>
[Table("AppUserProductPrice")]
public class AppUserProductPriceModel :BaseEntity
{
    
    [Required, Precision(18,4), Range(0, 100000)]
    public required decimal Price { get; set; }

    public string Currency {get; set;}= "ARS";
        
    [Required]
    public required int ProductSpecId { get; set; }
    [Required]
    public ProductSpecModel ProductSpecModel { get; set; }
    
    [Required]
    public required int StoreId  { get; set; }
    [Required]
    public StoreCoreModel StoreSpec {get; set;}
    
    [Required]
    public required DateTime ValidFrom { get; set; }
    
    public DateTime? ValidTo { get; set; } 

}