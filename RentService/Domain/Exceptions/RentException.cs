using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Exceptions
{
    public class RentException : Exception
    {
        public Guid ClientGuid { get; }
        public Vin CarVin { get; }

        public RentException()
        {
        }

        public RentException(string message) : base(message)
        {
        }

        public RentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RentException(string message, Guid clientGuid, Vin carVin) : base(message)
        {
            ClientGuid = clientGuid;
            CarVin = carVin;
        }

        public RentException(string message, Exception innerException, Guid clientGuid, Vin carVin) : base(message, innerException)
        {
            ClientGuid = clientGuid;
            CarVin = carVin;
        }
    }
}
