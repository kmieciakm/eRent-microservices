using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure.Client
{
    /// <summary>
    /// Specifies access point for Client entity.
    /// </summary>
    public interface IClientQuery
    {
        ClientEntity GetClient(Guid clientGuid);
        ClientEntity GetClient(string email);
        IEnumerable<ClientEntity> GetClients();
    }
}
