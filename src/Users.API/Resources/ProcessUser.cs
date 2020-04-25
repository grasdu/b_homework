namespace Users.API.Resources
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using Users.API.Domain.Models.Converters;
    using Users.API.Domain.Models.Enums;

    public class ProcessUser
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [JsonConverter(typeof(DateTimeStringJsonConverter))]
        public DateTime DateOfBirth { get; set; }

        public UserAccessLevel AccessLevel { get; set; }

    }
}
