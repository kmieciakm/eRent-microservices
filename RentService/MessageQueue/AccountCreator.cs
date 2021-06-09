using Domain.Ports.Infrastructure;
using Domain.Ports.Presenters;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace MessageQueue
{
    public class AccountCreator : IAutomaticAccountCreator, IDisposable
    {
        private ConnectionFactory _Factory { get; }
        private IConnection _Connection { get; set; }
        private IModel _Channel { get; set; }
        private IClientService _ClientService { get; }

        public AccountCreator(IClientService clientService)
        {
            _ClientService = clientService;
            _Factory = new ConnectionFactory
            {
                HostName = "my-rabbit",
                Port = 5672,
                UserName = "rabbitmq",
                Password = "rabbitmq"
                // Uri = new Uri("amqp://rabbitmq:rabbitmq@localhost:5672")
            };
            _Connection = _Factory.CreateConnection();
            _Channel = _Connection.CreateModel();
        }

        public void ListenRequests()
        {
            _Channel.QueueDeclare("account-queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            var consumer = new EventingBasicConsumer(_Channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                dynamic message = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(body));
                _ClientService.CreateClient(
                    Guid.Parse(message.Guid.ToString()),
                    message.Firstname.ToString(),
                    message.Lastname.ToString(),
                    message.Email.ToString());
            };

            _Channel.BasicConsume("account-queue", true, consumer);
        }

        public void Dispose()
        {
            _Channel?.Close();
            _Connection?.Close();
        }
    }
}
