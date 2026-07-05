using System.ComponentModel.DataAnnotations;

namespace ProductSeeker;

public class StoreWSpecDTO
{
    [Required(ErrorMessage = "Name of the business has to be submitted"), MaxLength(50)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Field has to be submitted"), MaxLength(50)]
    public required string Field { get; set; }

    public string? Description { get; set; }

    [Required(ErrorMessage = "Latituded must be submitted")]
    public double? Latitude { get; set; }
    [Required(ErrorMessage = "Longitude must be submitted")]
    public double? Longitude { get; set; }

    public string? BusinessDays { get; set; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }

}
