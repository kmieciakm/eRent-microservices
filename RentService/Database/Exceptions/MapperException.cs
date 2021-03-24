using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Database.Exceptions
{
    class MapperException : Exception
    {
        public MapperException()
        {
        }

        public MapperException(string message) : base(message)
        {
        }

        public MapperException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
