namespace Users.Logger.Messaging
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Users.Logger.Messaging.Models;

    public class SubscriberBackgroundService : BackgroundService
    {
        private readonly ISubscriber subscriber;
        private readonly IMessagesRepository messagesRepository;
        private readonly ILogger<SubscriberBackgroundService> logger;

        public SubscriberBackgroundService(ISubscriber subscriber, IMessagesRepository messagesRepository, ILogger<SubscriberBackgroundService> logger)
        {
            this.messagesRepository = messagesRepository ?? throw new ArgumentNullException(nameof(messagesRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            this.subscriber = subscriber ?? throw new ArgumentNullException(nameof(subscriber));
            this.subscriber.OnMessage += OnMessage;
        }

        private void OnMessage(object sender, Message message)
        {
            var json = JsonConvert.SerializeObject(message);
            logger.LogInformation($"New message: {json.ToString()}");

            messagesRepository.Add(message);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            subscriber.Start();

            return Task.CompletedTask;
        }
    }
}
