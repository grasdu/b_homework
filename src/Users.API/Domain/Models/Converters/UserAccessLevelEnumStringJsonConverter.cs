namespace Users.API.Domain.Models.Converters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Users.API.Domain.Models.Enums;

    public class UserAccessLevelEnumStringJsonConverter : JsonConverter<UserAccessLevel>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(UserAccessLevel);
        }
        public override UserAccessLevel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            UserAccessLevel accessLevel;
            Enum.TryParse<UserAccessLevel>(reader.GetString(), out accessLevel);
            return accessLevel;
        }

        public override void Write(Utf8JsonWriter writer, UserAccessLevel value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString().ToUpperInvariant());
        }

    }
}
