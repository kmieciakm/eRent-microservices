using Domain.Infrastructure;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Web;

namespace MessageQueue
{
    public class MailingMQ : IMailSender
    {
        private ConnectionFactory _Factory { get; }
        private MailingSettings _Settings { get; }

        public MailingMQ(IOptions<MailingSettings> mailingSettings)
        {
            _Settings = mailingSettings.Value;
            _Factory = new ConnectionFactory
            {
                HostName = "my-rabbit",
                Port = 5672,
                UserName = "rabbitmq",
                Password = "rabbitmq"
                //Uri = new Uri("amqp://rabbitmq:rabbitmq@localhost:5672")
            };
        }

        public void SendConfirmationEmail(string to, string token)
        {
            using var connection = _Factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("mailing-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var tokenEncoded = HttpUtility.UrlEncode(token);
            string tokenUrl = $"{_Settings.AccountUrl}confirm?email={to}&token={tokenEncoded}";

            var message = new { Name = "Send Confirmation Email", To = to, Token = tokenUrl };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("", "mailing-queue", null, body);
        }
    }
}
