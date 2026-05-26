using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProductSeeker;
using ProductSeeker.Data.Models;

public class POSTFoodProductWCoreDTO : POSTProductWCoreDTO
{
    [Required]
    [Range(0, 100000)]
    public float NetContent { get; set; }


    [JsonConverter(typeof(UnitJsonConverter))]
    [Required]
    public UnitOfMeasureEnum.Unit UnitOfMeasure { get; set; }

    public bool? TACC { get; set; }

    public override CategoriesEnum.ProductCategories Category => CategoriesEnum.ProductCategories.Food;

    public override List<object> GetSpecIdentifier() => new List<object> { NetContent, UnitOfMeasure};
 
}