using Domain.Services.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        private System.Timers.Timer _Timer { get; }

        public MailingServiceWorker(ILogger<MailingServiceWorker> logger, IAutomaticMailSender mailSender)
        {
            _Logger = logger;
            _MailSender = mailSender;
            _Timer = new System.Timers.Timer(15000);
            _Timer.Elapsed += (sender, e) =>
            {
                _Logger.LogInformation($"[Mailing Service] - Connecting to RabbitMQ ...");
                _MailSender.Connect();
                _Logger.LogInformation($"[Mailing Service] - Connected.");
            };
            _Timer.AutoReset = false;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _Logger.LogInformation($"[Mailing Service] - Start Backgroud Service ...");
            _Timer.Start();
            return Task.CompletedTask;
        }
    }
}
