using Domain.DomainModels;
using Domain.Exceptions;
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
        private IClientCreate _ClientCreate { get; }

        public ClientService(IClientQuery clientQuery, IClientCreate clientCreate)
        {
            _ClientQuery = clientQuery;
            _ClientCreate = clientCreate;
        }

        public IEnumerable<ClientEntity> GetClients()
        {
            return _ClientQuery.GetClients();
        }

        public ClientEntity GetClientByEmail(string email)
        {
            return _ClientQuery.GetClient(email);
        }

        public ClientEntity CreateClient(Guid clientGuid, string firstname, string lastname, string email)
        {
            var client = new ClientEntity(
                    clientGuid,
                    firstname,
                    lastname,
                    email
                );

            var result = _ClientCreate.Create(client);
            if (result == true)
            {
                var createdClient = _ClientQuery.GetClient(clientGuid);
                return createdClient;
            }
            else
            {
                throw new ClientException("Creation of user account failed unexpectedly!");
            }
        }
    }
}
