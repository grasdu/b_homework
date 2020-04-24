namespace Users.API.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Users.API.Domain.Models;

    public interface IUserService
    {
        Task<IEnumerable<User>> ListAsync();
    }
}
