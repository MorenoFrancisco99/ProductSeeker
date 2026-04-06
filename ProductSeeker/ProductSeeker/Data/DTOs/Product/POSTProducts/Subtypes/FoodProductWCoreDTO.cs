using System.ComponentModel.DataAnnotations;
using ProductSeeker;
using ProductSeeker.Data.Models;

public class FoodProductWCoreDTO : POSTProductWCoreDTO
{
    [Required]
    [Range(0, 100000)]
    public int NetContent { get; set; }

    [Required]
    public UnitOfMeasureEnum.Unit UnitOfMeasure { get; set; }

    public bool? TACC { get; set; }

    public override CategoriesEnum.ProductCategories Category => CategoriesEnum.ProductCategories.Food;

    public override List<object> GetSpecIdentifier() => new List<object> { NetContent, UnitOfMeasure};
 

    public override ProductSpecModel SpecToModel(string userID, int coreId) =>
        new FoodProductModel
        {
            Category = Category,
            ProductCoreId = coreId,
            NetContent = NetContent,
            UnitOfMeasure = UnitOfMeasure,
            TACC = TACC,
            IdCreator = userID,
            EAN = EAN,
            CreationSource = CreationSourceEnum.CreationSource.User
        };
}