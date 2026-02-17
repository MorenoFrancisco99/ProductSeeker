using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductSeeker.Data.Utils;

namespace ProductSeeker.Data.Models;


[Table("ProductSpecs")]
public class ProductSpecModel : BaseEntity
{
    
    [Required]
    public required string Category { get; set; }

    [Required] 
    public List<ProductSpecAttributeValue> Attributes { get; set; } = [];
    
    [Required]
    public required int ProductCoreId { get; set; }

    [Required] public required ProductCoreModel ProductCore { get; set; } = null!;

    public ICollection<AppUserProductPriceModel> Prices { get; set; } = new List<AppUserProductPriceModel>();
    public UnitStateEnum.UnitState State {get; set;}

    
}