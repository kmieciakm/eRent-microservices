using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Presenters
{
   public interface ICarService
   {
        CarEntity GetCar(Vin vin);
   }
}
