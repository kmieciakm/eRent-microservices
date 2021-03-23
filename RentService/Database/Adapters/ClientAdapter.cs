using Database.Helpers;
using Database.Repositories.Contracts;
using Domain.DomainModels;
using Domain.Ports.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Adapters
{
    class ClientAdapter : IClient
    {
        private IClientRepository _ClientRepository { get; set; }
        public ClientAdapter(IClientRepository clientRepository)
        {
            _ClientRepository = clientRepository;
        }

        public bool Create(ClientEntity client)
        {
            var dbClient = EntitiesMapper.MapToDbClientEntity(client);
            return _ClientRepository.CreateAndSave(dbClient);
        }

        public bool Delete(Guid clientGuid)
        {
            throw new NotImplementedException();
        }

        public ClientEntity Get(Guid clientGuid)
        {
            throw new NotImplementedException();
        }

        public bool Update(ClientEntity client)
        {
            throw new NotImplementedException();
        }
    }
}
