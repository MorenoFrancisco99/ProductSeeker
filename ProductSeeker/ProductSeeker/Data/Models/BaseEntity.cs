using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSeeker.Data.Models;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
    
    public bool IsActive { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreationDate { get; set; }


    [Required]
    public required int IdCreator { get; set; }
   
    [Required, ForeignKey(nameof(IdCreator))]
    public required AppUser Creator { get; set; } = null!;

}