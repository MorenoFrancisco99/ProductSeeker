using System.ComponentModel.DataAnnotations;

namespace ProductSeeker;

public class StoreCoreDTO 
{
    [Required(ErrorMessage = "Name of the business has to be submitted"), MaxLength(50)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Field has to be submitted"), MaxLength(50)]
    public required string Field { get; set; }
  
}
