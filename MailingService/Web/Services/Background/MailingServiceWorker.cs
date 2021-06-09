using Domain.Services.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Services.Background
{
    public class MailingServiceWorker : BackgroundService
    {
        private ILogger<MailingServiceWorker> _Logger { get; }
        private IAutomaticMailSender _MailSender { get; }

        public MailingServiceWorker(ILogger<MailingServiceWorker> logger, IAutomaticMailSender mailSender)
        {
            _Logger = logger;
            _MailSender = mailSender;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _Logger.LogInformation($"Start - Mailing Backgroud Service");
            _MailSender.ListenRequests();
            return Task.CompletedTask;
        }
    }
}
