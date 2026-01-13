using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSeeker.Data.Models;

[Table("ProductCore")]
public class ProductCoreModel : BaseEntity
{
   
    
    [Required]
    [MaxLength(50)]
    public required string ProductName { get; set; }

    [Required, MaxLength(50)]
    public required string Brand { get; set; }

    public List<ProductSpecModel> ProductSpecs { get; set; } = [];

}