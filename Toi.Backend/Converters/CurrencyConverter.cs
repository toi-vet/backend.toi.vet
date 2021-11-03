using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Toi.Backend.Models;

namespace Toi.Backend.Converters
{
    public class CurrencyConverter : JsonConverter<Currency>
    {
        public override Currency Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.GetString() ?? string.Empty;

        public override void Write(Utf8JsonWriter writer, Currency value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value);
    }
}