namespace Users.API.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Users.API.Domain.Models;

    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync(); 
    }
}
