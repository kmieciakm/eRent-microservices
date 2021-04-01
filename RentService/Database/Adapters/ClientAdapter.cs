using Database.Helpers.Mappers;
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
            var dbClient = Mapper.Client.MapToDbClientEntity(client);
            return _ClientRepository.CreateAndSave(dbClient);
        }

        public bool Delete(Guid clientGuid)
        {
            return _ClientRepository.DeleteAndSave(clientGuid);
        }

        public ClientEntity Get(Guid clientGuid)
        {
            var dbClient = _ClientRepository.GetClient(clientGuid);
            return Mapper.Client.MapToClientEntity(dbClient);
        }

        public bool Update(ClientEntity client)
        {
            var dbClient = Mapper.Client.MapToDbClientEntity(client);
            return _ClientRepository.UpdateAndSave(dbClient);
        }
    }
}
