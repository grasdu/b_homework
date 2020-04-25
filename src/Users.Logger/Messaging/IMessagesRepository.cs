namespace Users.Logger.Messaging
{
    using System.Collections.Generic;
    using Users.Logger.Messaging.Models;

    public interface IMessagesRepository
    {
        void Add(Message message);
        IReadOnlyCollection<Message> GetMessages();
    }
}