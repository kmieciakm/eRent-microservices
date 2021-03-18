using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure
{
    /// <summary>
    /// Specifies data access point for Rent entity.
    /// </summary>
    public interface IRent
    {
        RentEntity Get(Guid rentGuid);
        List<RentEntity> GetByClient(Guid clientGuid);
        bool Create(RentEntity rent);
        bool Update(RentEntity rent);
        bool Delete(Guid rentGuid);
    }
}
