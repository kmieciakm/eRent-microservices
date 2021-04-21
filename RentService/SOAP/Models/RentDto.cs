using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SOAP.Models
{
    [DataContract]
    public class RentDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public ClientDto Client { get; set; }
        [DataMember]
        public string StartDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }
        [DataMember]
        public string RentedVehicleVin { get; set; }
        [DataMember]
        public decimal TotalRentPrice { get; set; }

        public RentDto() { }

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
