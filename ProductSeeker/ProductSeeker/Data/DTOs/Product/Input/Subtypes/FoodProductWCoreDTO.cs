using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using ProductSeeker;
using ProductSeeker.Data.Models;


/// <summary>
/// DTO that inherits from ProductWCoreDTO
/// </summary>
public class POSTFoodProductWCoreDTO : POSTProductWCoreDTObase, IHasSpecPredicate<FoodProductModel>
{
    [Required]
    [Range(0, 100000)]
    public float NetContent { get; set; }


    [JsonConverter(typeof(UnitJsonConverter))]
    [Required]
    public UnitOfMeasureEnum.Unit UnitOfMeasure { get; set; }

    public bool? TACC { get; set; }

    public override CategoriesEnum.ProductCategories Category => CategoriesEnum.ProductCategories.Food;

    public Expression<Func<FoodProductModel, bool>> MatchPredicate =>
            spec => spec.NetContent == NetContent
                 && spec.UnitOfMeasure == UnitOfMeasure;
    public FoodProductModel ToEntity(int coreId)
    {
        throw new NotImplementedException();
    }
}