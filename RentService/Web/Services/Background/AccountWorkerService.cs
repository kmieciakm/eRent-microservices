using Domain.Ports.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Services.Background
{
    public class AccountWorkerService : BackgroundService
    {
        private ILogger<AccountWorkerService> _Logger { get; }
        private IAutomaticAccountCreator _AccountCreator { get; }
        private System.Timers.Timer _Timer { get; }

        public AccountWorkerService(ILogger<AccountWorkerService> logger, IAutomaticAccountCreator accountCreator)
        {
            _Logger = logger;
            _AccountCreator = accountCreator;
            _Timer = new System.Timers.Timer(15000);
            _Timer.Elapsed += (sender, e) =>
            {
                _Logger.LogInformation($"[Rent Service] - Connecting to RabbitMQ ...");
                _AccountCreator.Connect();
                _Logger.LogInformation($"[Rent Service] - Connected.");
            };
            _Timer.AutoReset = false;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _Logger.LogInformation($"[Rent Service] - Start Backgroud Service ...");
            _Timer.Start();
            return Task.CompletedTask;
        }
    }
}
