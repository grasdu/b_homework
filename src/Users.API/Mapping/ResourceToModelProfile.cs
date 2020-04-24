
namespace Users.API.Mapping
{
    using AutoMapper;
    using Users.API.Domain.Models;
    using Users.API.Resources;

    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<CreateUser, User>();
        }
    }
}

