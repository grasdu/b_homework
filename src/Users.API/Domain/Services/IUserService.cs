namespace Users.API.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Users.API.Domain.Models;
    using Users.API.Domain.Services.Communication;

    public interface IUserService
    {
        Task<IEnumerable<User>> ListAsync();
        Task<CreateUserResponse> CreateAsync(User user);
    }
}
