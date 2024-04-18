using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace siwar.Data.Infrastructure.Configuration;

/// <summary>
/// Converts an object to and from a Json string.
/// </summary>
public class ObjectToJsonConverter<T> : ValueConverter<T, string>
{
    private static JsonSerializerOptions Options = new JsonSerializerOptions();

    public ObjectToJsonConverter(ConverterMappingHints mappingHints = null)
        : base(v => JsonSerializer.Serialize(v, Options), v => JsonSerializer.Deserialize<T>(v, Options), mappingHints)
    {
        Options.Converters.Add(new JsonStringEnumConverter());
    }
}
