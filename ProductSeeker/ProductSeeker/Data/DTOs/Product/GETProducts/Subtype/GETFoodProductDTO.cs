using ProductSeeker;

namespace ProductSeeker
{
    public class GETFoodProductDTO : GETProductSpecDTO
    {
        public double NetContent { get; set; }
        public UnitOfMeasureEnum.Unit UnitOfMeasure { get; set; }
        public bool? TACC { get; set; }
    }
}