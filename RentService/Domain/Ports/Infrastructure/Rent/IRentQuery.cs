using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure.Rent
{
    /// <summary>
    /// Specifies access point for Rent entity.
    /// </summary>
    public interface IRentQuery
    {
        RentEntity Get(Guid rentGuid);
        List<RentEntity> GetRentsOfClient(Guid clientGuid);
    }
}
