using Database.Repositories;
using Domain.DomainModels;
using Domain.Ports.Directive;
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

        public bool Create(Client client)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid clientGuid)
        {
            throw new NotImplementedException();
        }

        public Client Get(Guid clientGuid)
        {
            throw new NotImplementedException();
        }

        public bool Update(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
