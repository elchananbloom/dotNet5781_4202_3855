using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    internal class BusInTravelException : Exception
    {
        public BusInTravelException()
        {
        }

        public BusInTravelException(string message) : base(message)
        {
        }

        public BusInTravelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusInTravelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}