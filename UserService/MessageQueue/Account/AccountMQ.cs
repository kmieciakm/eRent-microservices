using Domain.Infrastructure;
using Domain.Models;
using Domain.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueue.Account
{
    public class AccountMQ : IAccountsManger, IDisposable
    {
        private static ConnectionFactory _Factory { get; set; }
        private static IConnection _Connection { get; set; }
        private static IModel _Channel { get; set; }
        private IServiceCollection _ServiceCollection { get; }

        public AccountMQ(IServiceCollection serviceCollection)
        {
            _ServiceCollection = serviceCollection;
        }

        public void Connect()
        {
            _Factory = new ConnectionFactory
            {
                HostName = "my-rabbit",
                Port = 5672,
                UserName = "rabbitmq",
                Password = "rabbitmq"
                //Uri = new Uri("amqp://rabbitmq:rabbitmq@localhost:5672")
            };
            _Connection = _Factory.CreateConnection();
            _Channel = _Connection.CreateModel();
            ListenRequests();
        }

        public void CreateAccount(User user)
        {
            _Channel.QueueDeclare("account-queue",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var message = new
            {
                Name = "Create account",
                OperationType = AccountOperation.CREATE_ACCOUNT,
                Guid = user.Guid,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email
            };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            _Channel.BasicPublish("", "account-queue", null, body);
        }

        public void ListenRequests()
        {
            _Channel.QueueDeclare("account-queue",
                 durable: true,
                 exclusive: false,
                 autoDelete: false,
                 arguments: null);

            var consumer = new EventingBasicConsumer(_Channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                dynamic message = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(body));
                var userGuid = Guid.Parse(message.Guid.ToString());
                var userEmail = message.Email.ToString();
                var operationType = message.OperationType.ToString();

                if (Enum.TryParse(operationType, out AccountOperation operation))
                {
                    switch (operation)
                    {
                        case AccountOperation.DELETE_ACCOUNT:
                            using (var sp = _ServiceCollection.BuildServiceProvider())
                            {
                                using (var scope = sp.CreateScope())
                                {
                                    var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
                                    accountService.DeleteAccountAsync(userGuid);
                                }
                            }
                            _Channel.BasicAck(e.DeliveryTag, false);
                            break;
                        case AccountOperation.CONFIRM_ACCOUNT:
                            using (var sp = _ServiceCollection.BuildServiceProvider())
                            {
                                using (var scope = sp.CreateScope())
                                {
                                    var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
                                    var mailSender = scope.ServiceProvider.GetRequiredService<IMailSender>();
                                    var accountConfirmationToken = tokenService.GenerateAccountConfirmationTokenAsync(userGuid).Result;
                                    mailSender.SendConfirmationEmail(userEmail, accountConfirmationToken);
                                }
                            }
                            _Channel.BasicAck(e.DeliveryTag, false);
                            break;
                        default:
                            _Channel.BasicNack(e.DeliveryTag, false, true);
                            break;
                    }
                }
                else
                {
                    _Channel.BasicNack(e.DeliveryTag, false, false);
                }
            };

            _Channel.BasicConsume("account-queue", false, consumer);
        }

        public void Dispose()
        {
            _Channel?.Close();
            _Connection?.Close();
        }
    }
}
