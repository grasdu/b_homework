namespace Users.API.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Users.API.Domain.Models;
    using Users.API.Domain.Services.Communication;

    public interface IUserService
    {
        Task<IEnumerable<User>> ListAsync();
        Task<ProcessUserResponse> CreateAsync(User user);
        Task<ProcessUserResponse> UpdateAsync(int id, User user);
        Task<ProcessUserResponse> DeleteAsync(int id);
    }
}
