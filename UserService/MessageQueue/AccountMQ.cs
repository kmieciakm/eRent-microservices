using Domain.Infrastructure;
using Domain.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageQueue
{
    public class AccountMQ : IServicesManger, IDisposable
    {
        private ConnectionFactory _Factory { get; }
        private IConnection _Connection { get; set; }
        private IModel _Channel { get; set; }

        public AccountMQ()
        {
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

        public void CreateAccount(User user)
        {
            _Channel.QueueDeclare("account-queue",
                 durable: true,
                 exclusive: false,
                 autoDelete: false,
                 arguments: null);

            var message = new {
                Name = "Create account",
                Guid = user.Guid,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email
            };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            _Channel.BasicPublish("", "account-queue", null, body);
        }

        public void Dispose()
        {
            _Channel?.Close();
            _Connection?.Close();
        }
    }
}
