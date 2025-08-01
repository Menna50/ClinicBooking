﻿using System.Text.Json.Serialization;
using System.Text.Json;

namespace ClinicBooking.API.Converters
{
    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        private readonly string _format = "HH:mm:ss";

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeOnly.ParseExact(reader.GetString()!, _format);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }

}
