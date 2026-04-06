

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductSeeker.Data.Utils;

namespace ProductSeeker.Data.Models;

[Table("StoreSpecs")]
public class StoreSpecModel : BaseEntity
{
    [Required]
     public int StoreCoreId { get; set; }

    [Required] public StoreCoreModel Store { get; set; }

     public string? GeoLocation { get; set; }

    public string? BusinessDays {get; set;}

    [Required]
    public DateTime ValidFrom { get; set; }
    
    public DateTime? ValidTo { get; set; }
    
    public UnitStateEnum.UnitState State {get; set;}

    
}