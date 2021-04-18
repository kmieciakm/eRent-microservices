using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class RentDto
    {
        public Guid Id { get; }
        public ClientDto Client { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string RentedVehicleVin { get; set; }
        public decimal TotalRentPrice { get; set; }

        public RentDto(RentEntity rentEntity)
        {
            Id = rentEntity.RentGuid;
            Client = new ClientDto(rentEntity.Client);
            StartDate = rentEntity.RentalDate.ToString("G");
            EndDate = rentEntity.RentalDate.ToString("G");
            RentedVehicleVin = rentEntity.RentedVehicleVin.Value.ToString();
            TotalRentPrice = rentEntity.TotalRentPrice;
        }
    }
}
