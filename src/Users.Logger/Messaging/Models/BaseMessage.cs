namespace Users.Logger.Messaging.Models
{
    public abstract class BaseMessage
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }

        public BaseMessage()
        {

        }
        public BaseMessage(bool success, string message)
        {
            Success = success;
            this.Message = message;
        }
    }
}
