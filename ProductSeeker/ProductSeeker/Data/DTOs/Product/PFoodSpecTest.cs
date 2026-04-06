using System.Text.Json.Serialization;
using ProductSeeker;



public class PFoodSpecTestDTO : PCoreWSpecDTOTEST
{
    public int NetContent { get; set; }
    public UnitOfMeasureEnum.Unit UnitOfMeasure { get; set; }
}