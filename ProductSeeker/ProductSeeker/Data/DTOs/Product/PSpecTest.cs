

using System.Text.Json.Serialization;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(PFoodSpecTestDTO), typeDiscriminator: "Food")]
public class PSpectTestDTO
{
  public int ProductCoreId {get ; set;}
  public string EAN { get; set; }

}