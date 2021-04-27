using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain
{
    public class RegistrationException : Exception
    {
        public RegistrationException()
        {
        }

        public RegistrationException(string message) : base(message)
        {
        }

        public RegistrationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
