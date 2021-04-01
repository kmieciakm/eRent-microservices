using Domain.DomainModels;
using Domain.Ports.Infrastructure.Rent;
using Domain.Ports.Presenters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    class CarRentService : ICarRentService
    {
        private IRentQuery RentQuery { get; }
        public CarRentService(IRentQuery rent)
        {
            RentQuery = rent;
        }

        public IList<RentEntity> GetClientRents(Guid clientGuid)
        {
            return RentQuery.GetRentsOfClient(clientGuid);
        }
    }
}
