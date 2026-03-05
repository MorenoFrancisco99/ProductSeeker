using System.ComponentModel.DataAnnotations;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

 
public class FoodProductModel :ProductSpecModel
{
    [Required]
    [Range(0, 100000)]
    public required int NetContent {get; set;}

    [Required]
    public required UnitOfMeasureEnum.Unit UnitOfMeasure {get; set; }
    public bool? TACC {get; set;}

   
}
