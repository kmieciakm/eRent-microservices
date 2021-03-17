using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Entities
{
    class RentEnt
    {
        [Key]
        public Guid RentGuid { get; set; }
        public Guid ClientGuid { get; set; }
        [ForeignKey("ClientGuid")]
        public ClientEnt Client { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime EndRentalDate { get; set; }
        public string RentedVehicleVin { get; set; }
        public decimal TotalRentPrice { get; set; }
    }
}
