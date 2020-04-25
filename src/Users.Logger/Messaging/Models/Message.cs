namespace Users.Logger.Messaging.Models
{
    public class Message : BaseMessage
    {
        public User User { get; private set; }

        public Message(bool success, string errorMessage, User user) : base(success, errorMessage)
        {
            this.User = user;
        }
    }
}
