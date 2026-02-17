using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductSeeker.Data.Models;



[Table("ProductAliasesModel")]
public class ProductAliasModel: BaseEntity
{
    [Required]
    public int ProductCoreId { get; set; }

    public ProductCoreModel ProductCore { get; set; } = null!;

    [Required]
    public string Value { get; set; } = null!;

    public string Source { get; set; } = "user"; 
    // user | system | scraped
}
