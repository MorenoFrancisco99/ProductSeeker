using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProductSeeker;

public class POSTProductPriceDTO
{
    [Required, Precision(18,4)]
    public required decimal Price { get; set; }

    public required string Currency = "ARS";
        
    [Required]
    public required int ProductSpecId { get; set; }
    [Required]
    public required ProductSpecModel ProductSpecModel { get; set; }
    
    [Required]
    public required int StoreId  { get; set; }
    [Required]
    public required StoreCoreModel StoreSpec {get; set;}
    
    [Required]
    public required DateTime ValidFrom { get; set; }
    
    public DateTime? ValidTo { get; set; }
    
    [Required]
    public required DateTime PriceCreationDate {get ; set;}

}
