using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(POSTFoodProductDTO), typeDiscriminator: "Food")]
public abstract class POSTProductSpecDTObase
{

    [Required(ErrorMessage = "Core ID must be submitted")]
    public required int ProductCoreId { get; set; }
    public string? EAN { get; set; }

    [Required(ErrorMessage = "Must provide category of product")]
    public abstract CategoriesEnum.ProductCategories Category { get; }


}
