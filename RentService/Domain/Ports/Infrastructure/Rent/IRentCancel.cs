using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure.Rent
{
    /// <summary>
    /// Specifies cancelation Rent use case.
    /// </summary>
    public interface IRentCancel
    {
        bool Delete(Guid rentGuid);
    }
}
