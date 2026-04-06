using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductSeeker.Data.Utils;

namespace ProductSeeker.Data.Models;


[Table("ProductSpecs")]
public abstract class ProductSpecModel : BaseEntity
{

    public string? EAN { get; set; }

    [Required]
    public int ProductCoreId { get; set; }
    [Required]
    public CategoriesEnum.ProductCategories Category { get; set; }

    [Required]
     public ProductCoreModel ProductCore { get; set; } = null!;

    public ICollection<AppUserProductPriceModel> Prices { get; set; } = new List<AppUserProductPriceModel>();
    public UnitStateEnum.UnitState State {get; set;}
    
}