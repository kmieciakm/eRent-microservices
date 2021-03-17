using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure
{
    public interface ICarRentService
    {
        IList<Rent> GetClientRents(Guid clientGuid);
    }
}
