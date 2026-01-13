using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSeeker.Data.OldModels
{
    [Table("AppUserProducts")]
    public class AppUserProduct
    {
        public string? AppUserId { get; set; }
        public int ProductId { get; set; }
        public AppUser? AppUser { get; set; }
        public ProductModel? ProductModel { get; set; }
    }
}
