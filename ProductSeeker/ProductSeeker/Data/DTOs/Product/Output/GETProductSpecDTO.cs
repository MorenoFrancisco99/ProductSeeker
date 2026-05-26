using System.Text.Json.Serialization;
using ProductSeeker;


namespace ProductSeeker
{
   public abstract class GETProductSpecDTO
    {
        public int Id { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public CreationSourceEnum.CreationSource CreationSource { get; set; }
        public string IdCreator { get; set; } = null!;

        public CategoriesEnum.ProductCategories Category { get; set; }
        public string? EAN { get; set; }

        public int ProductCoreId { get; set; }

    }

}

