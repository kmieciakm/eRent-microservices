using Domain.Infrastructure;
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
        private IAccountsManger _AccountsManager { get; }
        private System.Timers.Timer _Timer { get; }

        public AccountWorkerService(ILogger<AccountWorkerService> logger, IAccountsManger accountsManager)
        {
            _Logger = logger;
            _AccountsManager = accountsManager;
            _Timer = new System.Timers.Timer(15000);
            _Timer.Elapsed += (sender, e) =>
            {
                _Logger.LogInformation($"[User Service] - Connecting to RabbitMQ ...");
                _AccountsManager.Connect();
                _Logger.LogInformation($"[User Service] - Connected.");
            };
            _Timer.AutoReset = false;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _Logger.LogInformation($"[User Service] - Start Backgroud Service ...");
            _Timer.Start();
            return Task.CompletedTask;
        }
    }
}
