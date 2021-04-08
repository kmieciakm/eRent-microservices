using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure.Car
{
    /// <summary>
    /// Specifies removing Car use case.
    /// </summary>
    public interface ICarDelete
    {
        bool Delete(Vin vin);
    }
}
