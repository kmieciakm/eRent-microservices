using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure
{
    /// <summary>
    /// Car rental management service.
    /// </summary>
    public interface ICarRentService
    {
        IList<Rent> GetClientRents(Guid clientGuid);
    }
}
