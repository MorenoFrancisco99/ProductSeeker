using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProductSeeker;

public class POSTProductPriceDTO
{
    [Required, Precision(18,4), Range(0, 100000)]
    public required decimal Price { get; set; }
        
    [Required]
    public required int ProductSpecId { get; set; }
    
    [Required]
    public required int StoreId  { get; set; }
    
    public DateTime? ValidFrom { get; set; }
    
    public DateTime? ValidTo { get; set; }

    
  
}
