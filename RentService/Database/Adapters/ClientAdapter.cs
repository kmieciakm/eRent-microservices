using Database.Repositories;
using Domain.DomainModels;
using Domain.Ports.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Adapters
{
    class ClientAdapter : IClient
    {
        private ClientRepository _ClientRepository { get; set; }
        public ClientAdapter(ClientRepository clientRepository)
        {
            _ClientRepository = clientRepository;
        }

        public bool Create(ClientEntity client)
        {
            throw new NotImplementedException();
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
