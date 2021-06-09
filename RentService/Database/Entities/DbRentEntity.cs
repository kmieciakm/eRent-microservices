using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Entities
{
    class DbRentEntity
    {
        [Key]
        public Guid RentGuid { get; set; }
        [Required]
        public Guid ClientGuid { get; set; }
        [ForeignKey("ClientGuid")]
        public DbClientEntity Client { get; set; }
        [Required]
        public DateTime RentalDate { get; set; }
        [Required]
        public DateTime EndRentalDate { get; set; }
        [Required]
        public string RentedVehicleVin { get; set; }
        [Required]
        public decimal TotalRentPrice { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DbRentEntity entity &&
                   RentGuid.Equals(entity.RentGuid) &&
                   ClientGuid.Equals(entity.ClientGuid) &&
                   Client.Equals(entity.Client) &&
                   RentalDate == entity.RentalDate &&
                   EndRentalDate == entity.EndRentalDate &&
                   RentedVehicleVin == entity.RentedVehicleVin &&
                   TotalRentPrice == entity.TotalRentPrice;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RentGuid, ClientGuid, Client, RentalDate, EndRentalDate, RentedVehicleVin, TotalRentPrice);
        }
    }
}
