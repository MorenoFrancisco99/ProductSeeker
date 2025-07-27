using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.DTOs
{
    public class NoStoreReferenceProductDTO
    {
        public int Id { get; set; }
        //FK
        public int StoreID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
    }
}
