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
        const string createdString = "User was created";
        const string updatedString = "User was updated";
        const string deletedString = "User was deleted";

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

                return new ProcessUserResponse(user, createdString);
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

            UpdateUser(existingUser,user);

            try
            {
                this.userRepository.Update(existingUser);
                await unitOfWork.CompleteAsync();

                return new ProcessUserResponse(existingUser, updatedString);
            }

            catch(Exception ex)
            {
                return new ProcessUserResponse($"An error occurred when updating the user with id: '{id}'. Error: '{ex.Message}'");
            }
        }

        public async Task<ProcessUserResponse> DeleteAsync(int id)
        {
            var existingUser = await this.userRepository.FindByIdAsync(id);

            if (existingUser == null)
            {
                return new ProcessUserResponse($"User was not found for id:'{id}'");
            }

            try
            {
                this.userRepository.Delete(existingUser);
                await unitOfWork.CompleteAsync();

                return new ProcessUserResponse(existingUser, deletedString);
            }

            catch (Exception ex)
            {
                return new ProcessUserResponse($"An error occurred when deleting the user with id: '{id}'. Error: '{ex.Message}'");
            }
        }

        #region privates

        private void UpdateUser(User existingUser, User newUser)
        {
            existingUser.Name = newUser.Name;
            existingUser.DateOfBirth = newUser.DateOfBirth;
            existingUser.AccessLevel = newUser.AccessLevel;
        }

        #endregion

    }
}

