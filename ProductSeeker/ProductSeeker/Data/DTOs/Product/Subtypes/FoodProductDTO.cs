using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

public class FoodProductDTO: ProductSpecDTO
{
    [Required(ErrorMessage ="Food attribute error: NetContent attribute must be submitted")]
    [Range(0, 100000)]
    public int NetContent {get; set;}

    [Required(ErrorMessage ="Food attribute error: UnitOfMeasure attribute must be submitted")]
    public UnitOfMeasureEnum.Unit UnitOfMeasure {get; set; }
    public bool? TACC {get; set;}

    public override ProductSpecModel ToModel(string userID)
    {
        

        return new FoodProductModel
            {
              Category = "Food",
              ProductCoreId = this.ProductCoreId,
              NetContent = this.NetContent,
              UnitOfMeasure = this.UnitOfMeasure,
              TACC = this.TACC,
              IdCreator = userID
            };
    }
}
