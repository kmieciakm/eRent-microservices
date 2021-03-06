using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Presenters
{
    public interface IClientService
    {
        IEnumerable<ClientEntity> GetClients();
        ClientEntity GetClientByEmail(string email);
        ClientEntity CreateClient(Guid clientGuid, string firstname, string lastname, string email);
    }
}
