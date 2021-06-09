using SOAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace SOAP.ServiceContracts
{
    [ServiceContract]
    public interface IRentSOAPService
    {
        [OperationContract]
        List<RentDto> GetRentalsOfClient(Guid clientId);
    }
}
