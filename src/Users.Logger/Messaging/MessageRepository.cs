namespace Users.Logger.Messaging
{
    using System;
    using System.Collections.Generic;
    using Users.Logger.Messaging.Models;

    public class InMemoryMessagesRepository : IMessagesRepository
    {
        private readonly Queue<Message> _messages;

        public InMemoryMessagesRepository()
        {
            _messages = new Queue<Message>();
        }

        public void Add(Message message)
        {
            _messages.Enqueue(message ?? throw new ArgumentNullException(nameof(message)));
        }

        public IReadOnlyCollection<Message> GetMessages()
        {
            return _messages.ToArray();
        }
    }
}