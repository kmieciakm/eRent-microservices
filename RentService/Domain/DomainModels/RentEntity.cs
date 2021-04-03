using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DomainModels
{
    /// <summary>
    /// Represent a car rental.
    /// </summary>
    public class RentEntity
    {
        public RentEntity(Guid rentGuid, ClientEntity client, DateTime rentalDate, DateTime endRentalDate, Vin rentedVehicleVin, decimal totalRentPrice)
        {
            bool isRentPeriodCorrect = IsValidRentPeriod(rentalDate, endRentalDate);
            if (!isRentPeriodCorrect)
            {
                throw new ArgumentException($"Given rent period is incorrect. Start day: {rentalDate}, last day: {endRentalDate}");
            }

            RentGuid = rentGuid;
            Client = client;
            RentalDate = rentalDate;
            EndRentalDate = endRentalDate;
            RentedVehicleVin = rentedVehicleVin;
            TotalRentPrice = totalRentPrice;
        }

        public Guid RentGuid { get; }
        public ClientEntity Client { get; private set; } // IMPROVEMENT: Change to client Guid ???
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
        public bool Expired { get { return DateTime.Compare(DateTime.Now, EndRentalDate) > 0; } }

        public void ExtendRentTime(int days)
        {
            // TODO: Validate parameter and check if rent does not expired
            EndRentalDate.AddDays(days);
            // TODO: Adjust TotalRentPrice
        }

        private static bool IsValidRentPeriod(DateTime startDate, DateTime endDate)
        {
            return DateTime.Compare(startDate, endDate) < 0;
        }

        public override bool Equals(object obj)
        {
            return obj is RentEntity rent &&
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
