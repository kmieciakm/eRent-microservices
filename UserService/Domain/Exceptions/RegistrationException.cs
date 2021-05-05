using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Exceptions
{
    public class RegistrationException : Exception
    {
        public ExceptionCause Cause { get; }

        public RegistrationException()
        {
        }

        public RegistrationException(string message, ExceptionCause cause = ExceptionCause.Unknown)
            : base(message)
        {
            Cause = cause;
        }

        public RegistrationException(string message, Exception innerException, ExceptionCause cause = ExceptionCause.Unknown)
            : base(message, innerException)
        {
            Cause = cause;
        }
    }
}
