using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProductSeeker;
using ProductSeeker.Data.Models;


/// <summary>
/// DTO
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(POSTFoodProductWCoreDTO), typeDiscriminator: "Food")]
public abstract class POSTProductWCoreDTO
{
    //Changes in MaxLength here has to be reflected on models 
    [Required, MaxLength(200)]
    public required string ProductName { get; set; }

    [Required, MaxLength(200)]
    public required string Brand { get; set; }
    public string? EAN { get; set; }
    
   
    public abstract CategoriesEnum.ProductCategories Category {get;}

    /// <summary>
    /// Returns a list of values that uniquely identify the product spec, used to check for duplicates before creation.
    /// </summary>
    /// <returns>A list of objects that uniquely identify the product spec</returns>
    public abstract List<object> GetSpecIdentifier();
}