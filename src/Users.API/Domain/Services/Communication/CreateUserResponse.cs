namespace Users.API.Domain.Services.Communication
{
    using Users.API.Domain.Models;

    public class CreateUserResponse : BaseResponse
    {
        public User User { get; private set; }

        private CreateUserResponse(bool success, string errorMessage, User user) : base(success, errorMessage)
        {
            this.User = user;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="user">Created user.</param>
        /// <returns>Response.</returns>
        public CreateUserResponse(User user) : this(true, string.Empty, user)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public CreateUserResponse(string errorMessage) : this(false, errorMessage, null)
        { }

    }
}
