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

        public async Task<ProcessUserResponse>CreateAsync(User user)
        {
            try
            {
                await userRepository.CreateAsync(user);
                await unitOfWork.CompleteAsync();

                return new ProcessUserResponse(user);
            }
            catch (Exception ex)
            {
                return new ProcessUserResponse($"An error occurred when creating the user: '{ex.Message}'");
            }
        }

        public async Task<ProcessUserResponse> UpdateAsync(int id, User user)
        {
            var existingUser = await this.userRepository.FindByIdAsync(id);

            if(existingUser == null)
            {
                return new ProcessUserResponse($"User was not found for id:'{id}'");
            }

            existingUser = user;

            try
            {
                this.userRepository.Update(existingUser);
                await unitOfWork.CompleteAsync();

                return new ProcessUserResponse(existingUser);
            }

            catch(Exception ex)
            {
                return new ProcessUserResponse($"An error occurred when updating the user with id: '{id}'. Error: '{ex.Message}'");
            }
        }

    }
}

