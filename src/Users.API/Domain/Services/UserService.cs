namespace Users.API.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Users.API.Domain.Models;
    using Users.API.Domain.Repositories;

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await userRepository.ListAsync();
        }
    }
}

