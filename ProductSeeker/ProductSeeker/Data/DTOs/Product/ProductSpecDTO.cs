using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(FoodProductDTO), typeDiscriminator: "Food")]
public abstract class ProductSpecDTO
{


    [Required(ErrorMessage ="Core ID must be submitted")]
    public required int ProductCoreId { get; set; }

    //New field in this base DTO required manually mapping in every child 
    //TODO code a proper maper
    public abstract ProductSpecModel ToModel(string userID);

}
