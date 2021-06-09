using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure.Client
{
    /// <summary>
    /// Specifies removing Client use case.
    /// </summary>
    public interface IClientDelete
    {
        bool Delete(Guid clientGuid);
    }
}
