using Domain.Ports.Directive;
using Domain.Ports.Infrastructure;
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
            (IClient client, IRent rent) => new CarRentService(client, rent);
    }
}
