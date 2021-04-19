using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Requests
{
    public class RentalRequest
    {
        public Guid ClientId { get; set; }
        public string CarVin { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
