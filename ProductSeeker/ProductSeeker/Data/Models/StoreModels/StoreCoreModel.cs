

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductSeeker.Data.Utils;

namespace ProductSeeker.Data.Models;

[Table("StoreCore")]
public class StoreCoreModel : BaseEntity
{
    [Required, MaxLength(50)]
    public required string Name {get; set;}
    
    [Required, MaxLength(50)]
    public required string Field {get; set;}

    [MaxLength(500)]
    public string? Description {get; set;}

    public UnitStateEnum.UnitState State {get; set;}

    public List<StoreSpecModel> StoreSpecs {get; set;}= [];

}