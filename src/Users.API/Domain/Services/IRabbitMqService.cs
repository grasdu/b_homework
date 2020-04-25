namespace Users.API.Domain.Services
{
    using Users.API.Domain.Services.Communication;

    public interface IRabbitMqService
    {
        void Send(ProcessUserResponse response);
    }
}
