using System.ComponentModel.DataAnnotations;

namespace ProductSeeker;

public class StoreSpecDTO
{
    [Required] 
    public int StoreCoreId { get; set; }

    public string? GeoLocation { get; set; }

    public string? BusinessDays {get; set;}
    public DateTime? ValidFrom {get; set;}
    public DateTime? ValidTo {get; set;}
}
