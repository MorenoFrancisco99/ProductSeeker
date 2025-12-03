using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.DTOs.Product
{
    /// <summary>
    /// General DTO for Product, used to transfer product data without store reference and ID.
    /// </summary>
    public class ProductDTO
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public float Price { get; set; } = 0.0f;
        public float Quantity { get; set; } = 0.0f;
        public string UnitType { get; set; } = string.Empty;
        public float? SubUnitQuantity { get; set; } = null; //Cuantas unidades trae el pack por ej. 6 latas de coca
        public string? SubUnitType { get; set; } = null; //Tipo de subunidad, por ej. Gr. Ml, Unidad, etc.
        public float? SubUnitAmount { get; set; } = null; //Cuantas latas de coca trae el pack por ej.

        public string ExtraInfo { get; set; } = string.Empty;
    }
}
