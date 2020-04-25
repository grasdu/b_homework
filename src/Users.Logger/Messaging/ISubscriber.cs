namespace Users.Logger.Messaging
{
    using System;
    using Users.Logger.Messaging.Models;

    public interface ISubscriber
    {
        void Start();
        event EventHandler<Message> OnMessage;
    }
}
