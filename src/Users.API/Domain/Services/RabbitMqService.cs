namespace Users.API.Domain.Services
{
    using RabbitMQ.Client;
    using System.Text;
    using System.Text.Json;
    using Users.API.Domain.Services.Communication;

    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnection connection;

        public RabbitMqService(IConnectionFactory connectionFactory)
        {
            this.connection = connectionFactory.CreateConnection();
        }

        public void Send(ProcessUserResponse response)
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "userQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var message = JsonSerializer.Serialize(response);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: "userQueue",
                                     basicProperties: null,
                                     body: body);
            }

        }

    }
}
