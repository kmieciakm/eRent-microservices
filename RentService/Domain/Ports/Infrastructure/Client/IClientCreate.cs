using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure.Client
{
    /// <summary>
    /// Specifies Client creation use case.
    /// </summary>
    public interface IClientCreate
    {
        bool Create(ClientEntity client);
    }
}
