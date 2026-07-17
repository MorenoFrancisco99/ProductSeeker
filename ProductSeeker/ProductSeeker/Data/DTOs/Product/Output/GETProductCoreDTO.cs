
using ProductSeeker;
using ProductSeeker.Data.Models;

public class GETProductCoreDTO
{
    public int Id { get; set; }
    
    // IsActive can be null to allow validation to catch missing value, but it will be set to true by default in the service layer when creating a new entity
    public bool? IsActive { get; set; }
    
    public DateTime CreationDate { get; set; }

    public  CreationSourceEnum.CreationSource CreationSource { get; set; }

    public string IdCreator { get; set; }
    public string ProductName { get; set; }

    public string Brand { get; set; }
    public CategoriesEnum.ProductCategories Category { get; set; }

    public List<GETProductSpecDTO> Specs { get; set; } = new List<GETProductSpecDTO>();
    
    public ICollection<ProductAliasModel> Aliases { get; set; } = new List<ProductAliasModel>();    

    
} 