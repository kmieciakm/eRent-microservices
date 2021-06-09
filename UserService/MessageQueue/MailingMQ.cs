﻿using Domain.Infrastructure;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Web;

namespace MessageQueue
{
    public class MailingMQ : IMailSender, IDisposable
    {
        private ConnectionFactory _Factory { get; }
        private IConnection _Connection { get; set; }
        private IModel _Channel { get; set; }
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
                // Uri = new Uri("amqp://rabbitmq:rabbitmq@localhost:5672")
            };
            _Connection = _Factory.CreateConnection();
            _Channel = _Connection.CreateModel();
        }

        public void SendConfirmationEmail(string to, string token)
        {
            _Channel.QueueDeclare("mailing-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var tokenEncoded = HttpUtility.UrlEncode(token);
            string tokenUrl = $"{_Settings.AccountUrl}confirm?email={to}&token={tokenEncoded}";

            var message = new { Name = "Send Confirmation Email", To = to, Token = tokenUrl };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            _Channel.BasicPublish("", "mailing-queue", null, body);
        }

        public void Dispose()
        {
            _Channel?.Close();
            _Connection?.Close();
        }
    }
}
