using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Exceptions
{
    public class EmailException : Exception
    {
        public string From { get; }
        public string To { get; }

        public EmailException()
        {
        }

        public EmailException(string message) : base(message)
        {
        }

        public EmailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public EmailException(string message, string from, string to) : base(message)
        {
            To = to;
            From = from;
        }

        public EmailException(string message, Exception innerException, string from, string to) : base(message, innerException)
        {
            To = to;
            From = from;
        }
    }
}
