
using ProductSeeker;
using ProductSeeker.Data.Models;

public class GETProductWSpecDTO
{
    public ProductCoreModel Core { get; set; } = null!;
    public POSTProductSpecDTO Spec { get; set; } = null!;
    
}