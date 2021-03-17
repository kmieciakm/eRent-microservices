using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Directive
{
    /// <summary>
    /// Specifies data access point for Client entity.
    /// </summary>
    public interface IClient
    {
        Client Get(Guid clientGuid);
        bool Create(Client client);
        bool Update(Client client);
        bool Delete(Guid clientGuid);
    }
}
