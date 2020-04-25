namespace Users.Logger.Messaging.Models
{
    using System;
    using System.Text.Json.Serialization;
    using Users.Logger.Messaging.Models.Enums;
    using Users.Logger.Messaging.Models.Converters;

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(DateTimeStringJsonConverter))]
        public DateTime DateOfBirth { get; set; }
        [JsonConverter(typeof(UserAccessLevelEnumStringJsonConverter))]
        public UserAccessLevel AccessLevel { get; set; }
    }
}
