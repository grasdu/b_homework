namespace Users.API.Domain.Services.Communication
{
    using Users.API.Domain.Models;

    public class ProcessUserResponse : BaseResponse
    {
        public User User { get; private set; }

        private ProcessUserResponse(bool success, string errorMessage, User user) : base(success, errorMessage)
        {
            this.User = user;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="user">Created user.</param>
        /// <returns>Response.</returns>
        public ProcessUserResponse(User user) : this(true, string.Empty, user)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ProcessUserResponse(string errorMessage) : this(false, errorMessage, null)
        { }

    }
}
