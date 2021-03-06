using Database.Helpers.Mappers;
using Database.Repositories.Contracts;
using Domain.DomainModels;
using Domain.Ports.Infrastructure.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Adapters
{
    class ClientAdapter : IClientQuery, IClientCreate, IClientModify, IClientDelete
    {
        private IClientRepository _ClientRepository { get; set; }

        public ClientAdapter(IClientRepository clientRepository)
        {
            _ClientRepository = clientRepository;
        }

        bool IClientCreate.Create(ClientEntity client)
        {
            var dbClient = Mapper.Client.MapToDbClientEntity(client);
            return _ClientRepository.CreateAndSave(dbClient);
        }

        bool IClientDelete.Delete(Guid clientGuid)
        {
            return _ClientRepository.DeleteAndSave(clientGuid);
        }

        ClientEntity IClientQuery.GetClient(Guid clientGuid)
        {
            var dbClient = _ClientRepository.Get(clientGuid);
            return Mapper.Client.MapToClientEntity(dbClient);
        }

        ClientEntity IClientQuery.GetClient(string email)
        {
            var dbClient = _ClientRepository.Get(email);
            return Mapper.Client.MapToClientEntity(dbClient);
        }

        IEnumerable<ClientEntity> IClientQuery.GetClients()
        {
            var dbClients = _ClientRepository.GetAll();
            return Mapper.Client.MapToClientEntity(dbClients);
        }

        bool IClientModify.Update(ClientEntity client)
        {
            var dbClient = Mapper.Client.MapToDbClientEntity(client);
            return _ClientRepository.UpdateAndSave(dbClient);
        }
    }
}
