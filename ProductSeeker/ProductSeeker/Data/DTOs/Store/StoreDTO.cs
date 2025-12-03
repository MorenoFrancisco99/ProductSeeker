using NetTopologySuite.Geometries;
using ProductSeeker.Data.DTOs.Product;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.DTOs.Store
{
    public class StoreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string BussinessDays { get; set; } = string.Empty; //Dias de atencion, por ej. Lunes a Sabado
        public float Rating { get; set; } = 0.0f; //Rating del store, por ej. 4.5
        public string ExtraInfo { get; set; } = string.Empty; //Informacion extra del store, por ej. Horarios de atencion, etc.
        public string Img { get; set; } = string.Empty; //Imagen del store, por ej. Logo o foto del local
        public double? Latitude { get; set; } = null;
        public double? Longitude { get; set; } = null;
        public string Address { get; set; } = string.Empty;

        public List<ProductDTO> ProductList { get; set; } = new List<ProductDTO>();
    }
}
