using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.DTOs
{
    /// <summary>
    /// Returns ProductDTO, ValidFrom and ValidTo
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class ProductHistoryDTO
    {
        public ProductDTO product { get; set; }
        public DateTime validFrom { get; set; }
        public DateTime validTo { get; set; }

    }
}
