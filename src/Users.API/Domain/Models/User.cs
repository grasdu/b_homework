namespace Users.API.Domain.Models
{
    using System;
    using Users.API.Domain.Models.Enums;

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserAccessLevel AccessLevel { get; set; }
    }
}
