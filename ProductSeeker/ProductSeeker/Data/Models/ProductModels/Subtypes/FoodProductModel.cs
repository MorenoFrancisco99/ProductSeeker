using System.ComponentModel.DataAnnotations;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

 
public class FoodProductModel :ProductSpecModel
{

    //Same value aplied as constrain in Validator
    //Modification here should be reflected in the validator and vice versa
    [Range(1, 100000)]
    public int NetContent {get; set;}

   
    public UnitOfMeasureEnum.Unit UnitOfMeasure {get; set; }
    public bool? TACC {get; set;}

}
