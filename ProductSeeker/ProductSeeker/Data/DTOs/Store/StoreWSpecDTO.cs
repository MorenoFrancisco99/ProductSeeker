using System.ComponentModel.DataAnnotations;

namespace ProductSeeker;

public class StoreWSpecDTO
{
     [Required(ErrorMessage ="Name of the business has to be submitted"), MaxLength(50)]
    public required string Name {get; set;}
    
    [Required(ErrorMessage ="Field has to be submitted"), MaxLength(50)]
    public required string Field {get; set;}
    [Required(ErrorMessage ="Creator ID must be set")] 
    public required string IdCreator {get; set;}
    public string? GeoLocation { get; set; }

    public string? BusinessDays {get; set;}
}
