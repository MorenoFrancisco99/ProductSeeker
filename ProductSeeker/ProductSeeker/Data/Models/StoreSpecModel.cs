

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSeeker.Data.Models;

[Table("StoreSpecs")]
public class StoreSpecModel : BaseEntity
{
    [Required] public required int StoreCoreId { get; set; }

    [Required] public required StoreCore Store { get; set; }

    [Required] public required string GeoLocation { get; set; }

    [Required]
    public required string BusinessDays {get; set;}

    [Required]
    public required DateTime ValidFrom { get; set; }
    
    public DateTime? ValidTo { get; set; }
    
    [Required]
    public required DateTime SpecCrationDate {get ; set;}
    
}