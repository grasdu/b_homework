namespace Users.API.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Users.API.Domain.Models;
    using Users.API.Domain.Repositories;
    using Users.API.Domain.Services.Communication;
    using Users.API.Persistence.Repositories;

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await userRepository.ListAsync();
        }
        public async Task<CreateUserResponse>CreateAsync(User user)
        {
            try
            {
                await userRepository.CreateAsync(user);
                await unitOfWork.CompleteAsync();

                return new CreateUserResponse(user);
            }
            catch (Exception ex)
            {
                return new CreateUserResponse($"An error occurred when creating the user: {ex.Message}");
            }
        }


    }
}

