using System.Text.Json;
using System.Text.Json.Serialization;
using static ProductSeeker.UnitOfMeasureEnum;


namespace ProductSeeker;

public class UnitJsonConverter : JsonConverter<Unit>
{
    public override Unit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.GetString()?.ToLowerInvariant() switch
        {
            "mg"                    => Unit.mg,
            "g"                     => Unit.g,
            "kg"                    => Unit.kg,
            "ml"                    => Unit.ml,
            "l"                     => Unit.l,
            "unit" or "u" or "un" or "units" or "un/s" or "unit/s"   => Unit.un, //for scrapping purposes
            var s => throw new JsonException($"Unknown Unit: '{s}'")
        };

    public override void Write(Utf8JsonWriter writer, Unit value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}