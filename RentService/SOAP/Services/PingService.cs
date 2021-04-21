using SOAP.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOAP.Services
{
    public class PingService : IPingService
    {
        public string Ping(string msg)
        {
            return string.Join(string.Empty, msg.Reverse());
        }
    }
}
