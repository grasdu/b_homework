namespace Users.Logger.Messaging
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Users.Logger.Messaging.Models;

    public class SubscriberBackgroundService : BackgroundService
    {
        private readonly ISubscriber _subscriber;
        private readonly IMessagesRepository _messagesRepository;
        private readonly ILogger<SubscriberBackgroundService> _logger;

        public SubscriberBackgroundService(ISubscriber subscriber, IMessagesRepository messagesRepository, ILogger<SubscriberBackgroundService> logger)
        {
            _messagesRepository = messagesRepository ?? throw new ArgumentNullException(nameof(messagesRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _subscriber = subscriber ?? throw new ArgumentNullException(nameof(subscriber));
            _subscriber.OnMessage += OnMessage;
        }

        private void OnMessage(object sender, Message message)
        {
            _logger.LogInformation($"got a new message: {message.Success}");

            _messagesRepository.Add(message);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriber.Start();

            return Task.CompletedTask;
        }
    }
}
