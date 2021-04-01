using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure.Rent
{
    /// <summary>
    /// Specifies Rent modification use case.
    /// </summary>
    public interface IRentModify
    {
        bool Update(RentEntity rent);
    }
}
