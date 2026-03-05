using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

[Table("MergedProductCores")]
public class MergedProductCore : BaseEntity
{
    public int? MergedIntoProductCoreId { get; set; }

    public ProductCoreModel? MergedInto { get; set; }
    
    public required string ProductName { get; set; }

    [Required, MaxLength(50)]
    public required string Brand { get; set; }
    public DateTime MergeDate { get; set; }
    
    public DateTime OriginalCreation { get; set; }



}
