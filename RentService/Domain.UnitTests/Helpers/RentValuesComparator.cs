using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Domain.UnitTests.Helpers
{
    /// <summary>
    /// Compares the Rent domain model values without checking the identity.
    /// </summary>
    public class RentValuesComparator : IEqualityComparer<RentEntity>
    {
        public bool Equals([AllowNull] RentEntity rent1, [AllowNull] RentEntity rent2)
        {
            return rent1.Client.Equals(rent2.Client) &&
                   rent1.RentalDate == rent2.RentalDate &&
                   rent1.EndRentalDate == rent2.EndRentalDate &&
                   rent1.RentDuration.Equals(rent2.RentDuration) &&
                   rent1.RentedVehicleVin.Equals(rent2.RentedVehicleVin) &&
                   rent1.TotalRentPrice == rent2.TotalRentPrice;
        }

        public int GetHashCode([DisallowNull] RentEntity rent)
        {
            return HashCode.Combine(
                rent.Client,
                rent.RentalDate,
                rent.EndRentalDate,
                rent.RentDuration,
                rent.RentedVehicleVin,
                rent.TotalRentPrice);
        }
    }
}
