using Domain.Ports.Presenters;
using SOAP.Models;
using SOAP.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOAP.Services
{
    public class RentSOAPService : IRentSOAPService
    {
        private ICarRentService _CarRentService { get; }

        public RentSOAPService(ICarRentService carRentService)
        {
            _CarRentService = carRentService;
        }

        public List<RentDto> GetRentalsOfClient(Guid clientId)
        {
            var rents = new List<RentDto>();
            _CarRentService
                .GetClientRentals(clientId).ToList()
                .ForEach(rent => rents.Add(new RentDto(rent)));

            return rents;
        }
    }
}
