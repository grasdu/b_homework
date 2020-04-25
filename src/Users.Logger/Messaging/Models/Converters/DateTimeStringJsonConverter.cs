namespace Users.Logger.Messaging.Models.Converters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    public class DateTimeStringJsonConverter : JsonConverter<DateTime>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateTime = reader.GetString();
            if (string.IsNullOrWhiteSpace(dateTime))
            {
                return default(DateTime);
            }
            DateTime parsedDate = DateTime.Parse(dateTime);

            return parsedDate.Date;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }


    }
}
