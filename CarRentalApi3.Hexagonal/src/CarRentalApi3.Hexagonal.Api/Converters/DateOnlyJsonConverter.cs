using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarRentalApi3.Hexagonal.Api.Converters;
public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString()!, "yyyy-MM-dd");
    }


    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
          writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}
