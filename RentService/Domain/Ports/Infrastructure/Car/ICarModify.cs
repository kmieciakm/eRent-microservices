using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure.Car
{
    /// <summary>
    /// Specifies Car modification use case.
    /// </summary>
    public interface ICarModify
    {
        bool Update(CarEntity car);
    }
}
