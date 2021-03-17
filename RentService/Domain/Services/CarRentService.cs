using Domain.DomainModels;
using Domain.Ports.Directive;
using Domain.Ports.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    class CarRentService : ICarRentService
    {
        public IClient Client { get; set; }
        public IRent Rent { get; set; }
        public CarRentService(IClient client, IRent rent)
        {
            Client = client;
            Rent = rent;
        }

        public IList<Rent> GetClientRents(Guid clientGuid)
        {
            return Rent.GetByClient(clientGuid);
        }
    }
}
