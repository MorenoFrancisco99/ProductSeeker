using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProductSeeker;
using ProductSeeker.Data.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(FoodProductWCoreDTO), typeDiscriminator: "Food")]
public abstract class POSTProductWCoreDTO
{
    [Required, MaxLength(50)]
    public required string ProductName { get; set; }

    [Required, MaxLength(50)]
    public required string Brand { get; set; }
    public string? EAN { get; set; }
    
    /// <summary>
    /// Converts the ProductSpecDTO to a ProductSpecModel, using the provided userID and coreId. The userID is used to set the IdCreator of the ProductSpecModel, and the coreId is used to set the ProductCoreId of the ProductSpecModel.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="coreId"></param>
    /// <returns>A ProductSpecModel instance</returns>
    public abstract ProductSpecModel SpecToModel(string userID, int coreId);
    
    public abstract CategoriesEnum.ProductCategories Category {get;}

    /// <summary>
    /// Returns a list of values that uniquely identify the product spec, used to check for duplicates before creation.
    /// </summary>
    /// <returns>A list of objects that uniquely identify the product spec</returns>
    public abstract List<object> GetSpecIdentifier();
}