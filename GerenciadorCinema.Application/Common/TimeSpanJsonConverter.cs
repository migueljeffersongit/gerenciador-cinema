using System.Text.Json;
using System.Xml;

namespace GerenciadorCinema.Application.Common;

public class TimeSpanJsonConverter : System.Text.Json.Serialization.JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();
        return XmlConvert.ToTimeSpan(stringValue);
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(XmlConvert.ToString(value));
    }
}
