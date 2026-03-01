using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSeeker.Data.Models;

[Table("ProductSpecAttributeValue")]
public class ProductSpecAttributeValue : BaseEntity
{
    [Required]
    public  int ProductSpecId { get; set; }
    [Required]
    public ProductSpecModel ProductSpec { get; set; } = null!;

    [Required]
    public required  string AttributeKey { get; set; }
    [Required]
    public required  string AttributeValue { get; set; }
}