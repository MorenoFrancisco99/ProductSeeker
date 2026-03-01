using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

public class ProductSpecDTO
{

    [Required]
    public required string Category { get; set; }

    [Required]
    public Dictionary<string, string> Attributes { get; set; }
    
    [Required]
    public required int ProductCoreId { get; set; }
}
