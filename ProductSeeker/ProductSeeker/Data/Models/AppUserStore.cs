using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSeeker.Data.Models
{
    [Table("AppUserStores")]
    public class AppUserStore
    {
        public string? AppUserId { get; set; }
        public int StoreId { get; set; }
        public AppUser? AppUser { get; set; }
        public StoreModel? StoreModel { get; set; }
    }
}
