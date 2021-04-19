using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Seed
{
    public class RentDbSeedSettings : ISeedSettings
    {
        public string ClientDataRelativePath { get; set; }
        public string RentsDataRelativePath { get; set; }
        public string CarsDataRelativePath { get; set; }
    }
}
