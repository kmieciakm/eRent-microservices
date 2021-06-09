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

        public AccountWorkerService(ILogger<AccountWorkerService> logger, IAutomaticAccountCreator accountCreator)
        {
            _Logger = logger;
            _AccountCreator = accountCreator;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _Logger.LogInformation($"Start - Account Backgroud Service");
            _AccountCreator.ListenRequests();
            return Task.CompletedTask;
        }
    }
}
