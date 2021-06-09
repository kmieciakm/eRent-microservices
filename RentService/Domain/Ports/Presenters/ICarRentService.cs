using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Presenters
{
    /// <summary>
    /// Car rental management service.
    /// </summary>
    public interface ICarRentService
    {
        /// <summary>
        /// Returns rental of the given GUID.
        /// </summary>
        /// <param name="rentalGuid">Rental identificator.</param>
        /// <returns>Rental information.</returns>
        RentEntity GetRental(Guid rentalGuid);
        /// <summary>
        /// Returns all rentals of the given client.
        /// </summary>
        /// <param name="clientGuid">Client identificator.</param>
        /// <returns>Clients rentals.</returns>
        IList<RentEntity> GetClientRentals(Guid clientGuid);
        /// <summary>
        /// Creates new rental of a given car for the client.
        /// </summary>
        /// <param name="clientGuid">Client identificator.</param>
        /// <param name="carVin">VIN number of the rented car.</param>
        /// <param name="startRentDate">The first day of the rental.</param>
        /// <param name="endRentDate">The last day of the rental.</param>
        /// <returns>Rental information.</returns>
        RentEntity RentCar(Guid clientGuid, Vin carVin, DateTime startRentDate, DateTime endRentDate);
    }
}
