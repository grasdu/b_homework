namespace Users.API.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Users.API.Domain.Models;

    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync();
        Task CreateAsync(User user);
        Task<User> FindByIdAsync(int id);
        void Update(User user);
        void Delete(User user);
    }
}
