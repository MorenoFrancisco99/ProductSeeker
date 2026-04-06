using System.ComponentModel.DataAnnotations;

namespace ProductSeeker;

public class POSTProductCoreDTO
{  
    [Required(ErrorMessage ="Must provide name of product")]
    [MaxLength(100)]
    public required string ProductName { get; set; }
     [Required(ErrorMessage ="Must provide category of product")]
    public required CategoriesEnum.ProductCategories Category { get; set; }

    [Required(ErrorMessage ="Must provide brand of product"), MaxLength(50)]
    public required string Brand { get; set; }
}
