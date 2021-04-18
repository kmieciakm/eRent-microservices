using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure.Car
{
    /// <summary>
    /// Specifies Car creation use case.
    /// </summary>
    public interface ICarCreate
    {
        bool Create(CarEntity car);
    }
}
