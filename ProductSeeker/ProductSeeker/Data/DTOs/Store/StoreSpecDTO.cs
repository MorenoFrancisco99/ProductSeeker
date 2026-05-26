using System.ComponentModel.DataAnnotations;

namespace ProductSeeker;

public class StoreSpecDTO
{
    [Required(ErrorMessage = "StoreCoreId is required.")] 
    public int StoreCoreId { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? BusinessDays {get; set;}
    public DateTime? ValidFrom {get; set;}
    public DateTime? ValidTo {get; set;}
}
