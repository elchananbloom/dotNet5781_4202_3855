using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    internal class BusLineException : Exception
    {
        public BusLineException()
        {
        }

        public BusLineException(string message) : base(message)
        {
        }

        public BusLineException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusLineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}