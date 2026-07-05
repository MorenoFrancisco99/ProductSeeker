using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProductSeeker.Data.Models;

namespace ProductSeeker;


/// <summary>
/// DTO that inherits from polimorphic ProductSpectDTO
/// </summary>
public class POSTFoodProductDTO : POSTProductSpecDTO
{
    [Required(ErrorMessage = "Food attribute error: NetContent attribute must be submitted")]
    [Range(0, 100000)]
    public float NetContent { get; set; }

    
    [JsonConverter(typeof(UnitJsonConverter))]
    [Required(ErrorMessage = "Food attribute error: UnitOfMeasure attribute must be submitted")]
    public UnitOfMeasureEnum.Unit UnitOfMeasure { get; set; }
    public bool? TACC { get; set; }

    public override CategoriesEnum.ProductCategories Category => CategoriesEnum.ProductCategories.Food;

 
}
