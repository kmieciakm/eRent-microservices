using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure.Rent
{
    /// <summary>
    /// Specifies Rent creation use case.
    /// </summary>
    public interface IRentCreator
    {
        bool Create(RentEntity rent);
    }
}
