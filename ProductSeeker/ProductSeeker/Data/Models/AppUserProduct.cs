using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSeeker.Data.Models
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
