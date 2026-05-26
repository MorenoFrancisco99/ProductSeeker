using System.ComponentModel.DataAnnotations;

namespace ProductSeeker;

public class POSTProductCoreDTO
{
    //Changes in MaxLength here has to be reflected on models 

    [Required(ErrorMessage = "Must provide name of product")]
    [MaxLength(200)]
    public required string ProductName { get; set; }
    [Required(ErrorMessage = "Must provide category of product")]
    public required CategoriesEnum.ProductCategories Category { get; set; }

    [Required(ErrorMessage = "Must provide brand of product"), MaxLength(200)]
    public required string Brand { get; set; }
}
