using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSeeker.Data.Models;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
    
    // IsActive can be null to allow validation to catch missing value, but it will be set to true by default in the service layer when creating a new entity
    public bool? IsActive { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreationDate { get; set; }

    [Required]
    public  CreationSourceEnum.CreationSource CreationSource { get; set; }

    [Required]
    public string IdCreator { get; set; }


}