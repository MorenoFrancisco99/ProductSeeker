using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using ProductSeeker;
using ProductSeeker.Data.Models;


/// <summary>
/// Input DTO composed of Core fields, Spec and a respective type based on an $type field
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(POSTFoodProductWCoreDTO), typeDiscriminator: "Food")]
public abstract class POSTProductWCoreDTObase 
{
    //Changes in MaxLength here has to be reflected on models 
    [Required, MaxLength(200)]
    public required string ProductName { get; set; }

    [Required, MaxLength(200)]
    public required string Brand { get; set; }
    public string? EAN { get; set; }
    public abstract CategoriesEnum.ProductCategories Category { get; }


}