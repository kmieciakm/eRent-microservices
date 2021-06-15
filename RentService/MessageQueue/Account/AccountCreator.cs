using Domain.Ports.Infrastructure;
using Domain.Ports.Presenters;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace MessageQueue.Account
{
    public class AccountCreator : IAutomaticAccountCreator, IDisposable
    {
        private ConnectionFactory _Factory { get; set; }
        private IConnection _Connection { get; set; }
        private IModel _Channel { get; set; }
        private IClientService _ClientService { get; }

        public AccountCreator(IClientService clientService)
        {
            _ClientService = clientService;
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
                var operationType = message.OperationType.ToString();

                if (Enum.TryParse(operationType, out AccountOperation operation))
                {
                    switch (operation)
                    {
                        case AccountOperation.CREATE_ACCOUNT:
                            try
                            {
                                _ClientService.CreateClient(
                                    Guid.Parse(message.Guid.ToString()),
                                    message.Firstname.ToString(),
                                    message.Lastname.ToString(),
                                    message.Email.ToString());

                                message.OperationType = AccountOperation.CONFIRM_ACCOUNT;
                                byte[] confirmBody = ParseMessage(message);
                                _Channel.BasicPublish("", "account-queue", null, confirmBody);
                                _Channel.BasicAck(e.DeliveryTag, false);
                            }
                            catch (Exception)
                            {
                                message.OperationType = AccountOperation.DELETE_ACCOUNT;
                                byte[] deleteBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                                _Channel.BasicPublish("", "account-queue", null, deleteBody);
                                _Channel.BasicAck(e.DeliveryTag, false);
                            }
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

        private static byte[] ParseMessage(dynamic message)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        }

        public void Dispose()
        {
            _Channel?.Close();
            _Connection?.Close();
        }
    }
}
