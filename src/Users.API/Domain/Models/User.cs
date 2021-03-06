﻿namespace Users.API.Domain.Models
{
    using System;
    using System.Text.Json.Serialization;
    using Users.API.Domain.Models.Converters;
    using Users.API.Domain.Models.Enums;

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
