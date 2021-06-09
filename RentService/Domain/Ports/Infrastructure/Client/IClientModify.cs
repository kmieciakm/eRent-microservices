using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure.Client
{
    /// <summary>
    /// Specifies Client modification use case.
    /// </summary>
    public interface IClientModify
    {
        bool Update(ClientEntity client);
    }
}
