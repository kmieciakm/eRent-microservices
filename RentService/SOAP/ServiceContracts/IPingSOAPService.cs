using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace SOAP.ServiceContracts
{
    [ServiceContract]
    public interface IPingSOAPService
    {
        [OperationContract]
        string Ping(string msg);
    }
}
