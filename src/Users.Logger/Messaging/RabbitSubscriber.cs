
namespace Users.Logger.Messaging
{
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;
    using Newtonsoft.Json;
    using Users.Logger.Messaging.Models;

    public class RabbitSubscriber : ISubscriber, IDisposable
    {
        private readonly IConnection connection;
        private IModel channel;
        private const string queueName = "userQueue";


        public RabbitSubscriber(IConnectionFactory connectionFactory)
        {
            this.connection = connectionFactory.CreateConnection();
        }

        private void InitChannel()
        {
            // _channel?.Dispose();

            channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            channel.CallbackException += (sender, ea) =>
            {
                InitChannel();
                InitSubscription();
            };
        }

        private void InitSubscription()
        {
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += OnMessageReceived;

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        private void OnMessageReceived(object sender, BasicDeliverEventArgs eventArgs)
        {
            var byteArray = eventArgs.Body.ToArray();
            var body = Encoding.UTF8.GetString(byteArray);
            var message = JsonConvert.DeserializeObject<Message>(body);

            this.OnMessage(this, message);
        }

        public event EventHandler<Message> OnMessage;

        public void Start()
        {
            InitChannel();
            InitSubscription();
        }

        public void Dispose()
        {
            channel?.Dispose();
        }
    }
}
