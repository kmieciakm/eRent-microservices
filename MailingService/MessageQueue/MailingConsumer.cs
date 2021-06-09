using Domain.Services.Contracts;
using Domain.Services.Infrastructure;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace MessageQueue
{
    public class MailingConsumer : IAutomaticMailSender, IDisposable
    {
        private ConnectionFactory _Factory { get; }
        private IConnection _Connection { get; set; }
        private IModel _Channel { get; set; }
        private IMailSender _MailSender { get; }

        public MailingConsumer(IMailSender mailSender)
        {
            _Factory = new ConnectionFactory
            {
                HostName = "my-rabbit",
                Port = 5672,
                UserName = "rabbitmq",
                Password = "rabbitmq"
                // Uri = new Uri("amqp://rabbitmq:rabbitmq@localhost:5672")
            };
            _MailSender = mailSender;
        }

        public void ListenRequests()
        {
            _Connection = _Factory.CreateConnection();
            _Channel = _Connection.CreateModel();

            _Channel.QueueDeclare("mailing-queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            var consumer = new EventingBasicConsumer(_Channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                dynamic message = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(body));
                _MailSender.SendConfirmationEmailAsync(
                    "uAppCorp@gmail.com",
                    message.To.ToString(),
                    "Confirm Your Account",
                    message.Token.ToString());
            };

            _Channel.BasicConsume("mailing-queue", true, consumer);
        }

        public void Dispose()
        {
            _Channel.Close();
            _Connection.Close();
        }
    }
}
