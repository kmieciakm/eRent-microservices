using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DomainModels
{
    /// <summary>
    /// Represent a car rental.
    /// </summary>
    public class Rent
    {
        public Rent(Guid rentGuid, Client client, DateTime rentalDate, DateTime endRentalDate, Vin rentedVehicleVin, decimal totalRentPrice)
        {
            RentGuid = rentGuid;
            Client = client;
            RentalDate = rentalDate;
            EndRentalDate = endRentalDate;
            RentedVehicleVin = rentedVehicleVin;
            TotalRentPrice = totalRentPrice;
        }

        public Guid RentGuid { get; }
        public Client Client { get; private set; }
        /// <summary>
        /// Specifies begining of a car rent.
        /// </summary>
        public DateTime RentalDate { get; private set; }
        /// <summary>
        /// Specifies end of the rent.
        /// </summary>
        public DateTime EndRentalDate { get; private set; }
        /// <summary>
        /// Specifies remaining time of the rent.
        /// </summary>
        public TimeSpan RentDuration { get { return EndRentalDate - RentalDate; } }
        public Vin RentedVehicleVin { get; }
        /// <summary>
        /// Price for the entire rental period.
        /// </summary>
        public decimal TotalRentPrice { get; }

        public override bool Equals(object obj)
        {
            return obj is Rent rent &&
                   RentGuid.Equals(rent.RentGuid) &&
                   Client.Equals(rent.Client) &&
                   RentalDate == rent.RentalDate &&
                   EndRentalDate == rent.EndRentalDate &&
                   RentDuration.Equals(rent.RentDuration) &&
                   RentedVehicleVin.Equals(rent.RentedVehicleVin) &&
                   TotalRentPrice == rent.TotalRentPrice;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RentGuid, Client, RentalDate, EndRentalDate, RentDuration, RentedVehicleVin, TotalRentPrice);
        }
    }
}
