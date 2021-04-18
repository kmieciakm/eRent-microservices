using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure.Car
{
    public interface ICarQuery
    {
        CarEntity Get(Vin vin);
        bool Exist(Vin vin);
    }
}
