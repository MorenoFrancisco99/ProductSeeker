using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

public class POSTFoodProductDTO : POSTProductSpecDTO
{
    [Required(ErrorMessage = "Food attribute error: NetContent attribute must be submitted")]
    [Range(0, 100000)]
    public int NetContent { get; set; }

    [Required(ErrorMessage = "Food attribute error: UnitOfMeasure attribute must be submitted")]
    public UnitOfMeasureEnum.Unit UnitOfMeasure { get; set; }
    public bool? TACC { get; set; }

    public override CategoriesEnum.ProductCategories Category => CategoriesEnum.ProductCategories.Food;

}
