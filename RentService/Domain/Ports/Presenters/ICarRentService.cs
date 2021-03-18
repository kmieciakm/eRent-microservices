using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Presenters
{
    /// <summary>
    /// Car rental management service.
    /// </summary>
    public interface ICarRentService
    {
        IList<RentEntity> GetClientRents(Guid clientGuid);
    }
}
