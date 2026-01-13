

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSeeker.Data.Models;

[Table("StoreCore")]
public class StoreCore : BaseEntity
{
    [Required, MaxLength(50)]
    public required string Name {get; set;}
    
    [Required, MaxLength(50)]
    public required string Field {get; set;}

    

    public List<StoreSpecModel> StoreSpecs = [];

}