using Domain.DomainModels;
using Domain.Ports.Infrastructure.Client;
using Domain.Ports.Presenters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    class ClientService : IClientService
    {
        private IClientQuery _ClientQuery { get; }

        public ClientService(IClientQuery clientQuery)
        {
            _ClientQuery = clientQuery;
        }

        public IEnumerable<ClientEntity> GetClients()
        {
            return _ClientQuery.GetClients();
        }

        public ClientEntity GetClientByEmail(string email)
        {
            return _ClientQuery.GetClient(email);
        }
    }
}
