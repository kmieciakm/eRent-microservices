using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure
{
    /// <summary>
    /// Specifies data access point for Client entity.
    /// </summary>
    public interface IClient
    {
        ClientEntity Get(Guid clientGuid);
        bool Create(ClientEntity client);
        bool Update(ClientEntity client);
        bool Delete(Guid clientGuid);
    }
}
