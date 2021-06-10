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
        private IAccountsManger _AccountsManager { get; }

        public AccountWorkerService(IAccountsManger accountsManager)
        {
            _AccountsManager = accountsManager;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _AccountsManager.Connect();
            return Task.CompletedTask;
        }
    }
}
