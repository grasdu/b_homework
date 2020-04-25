
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
        //private readonly IBusConnection _connection;
        private IModel _channel;
        private QueueDeclareOk _queue;

        private const string ExchangeName = "userQueue";

        public RabbitSubscriber()
        {
            //_connection = connection ?? throw new ArgumentNullException(nameof(connection));

        }

        private void InitChannel()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var _connection = factory.CreateConnection();
           // _channel?.Dispose();

            _channel = _connection.CreateModel();

           // _channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout);

           // _channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout);

            // since we're using a Fanout exchange, we don't specify the name of the queue
            // but we let Rabbit generate one for us. This also means that we need to store the
            // queue name to be able to consume messages from it
            _channel.QueueDeclare(queue: "userQueue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            //_channel.QueueBind(_queue.QueueName, ExchangeName, string.Empty, null);

            _channel.CallbackException += (sender, ea) =>
            {
                InitChannel();
                InitSubscription();
            };
        }

        private void InitSubscription()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += OnMessageReceived;

            _channel.BasicConsume(queue: "userQueue", autoAck: true, consumer: consumer);
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
            _channel?.Dispose();
        }
    }
}
