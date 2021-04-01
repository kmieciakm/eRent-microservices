using Domain.Ports.Infrastructure.Client;
using Domain.Ports.Infrastructure.Rent;
using Domain.Ports.Presenters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    /// <summary>
    /// Enables to retrive services implementations.
    /// </summary>
    public static class ServicesFactory
    {
        public static ICarRentService CreateCarRentService
            (IRentQuery rent) => new CarRentService(rent);
    }
}
