
using System.Text.Json.Serialization;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(PFoodSpecTestDTO), typeDiscriminator: "Food")]
public class PCoreWSpecDTOTEST
{
    public string ProductName { get; set; }
    public string Brand { get; set; }
    public string Category { get; set; }
    public int ProductCoreId { get; set; }
    public string EAN { get; set; }
}