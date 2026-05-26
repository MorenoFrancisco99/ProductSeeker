using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProductSeeker.Data.Utils;


namespace ProductSeeker.Data.Models;



[Table("ProductCore")]
public class ProductCoreModel : BaseEntity
{
   
    
    [Required]
    [MaxLength(200)]
    public string ProductName { get; set; }

    [Required, MaxLength(200)]
    public string Brand { get; set; }
     [Required]
    public CategoriesEnum.ProductCategories Category { get; set; }

    public List<ProductSpecModel> Specs { get; set; } = new List<ProductSpecModel>();
    
    public ICollection<ProductAliasModel> Aliases { get; set; } = new List<ProductAliasModel>();    
    public UnitStateEnum.UnitState State {get; set;}

}