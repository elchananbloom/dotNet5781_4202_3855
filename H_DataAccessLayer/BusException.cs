using System;
using System.Runtime.Serialization;

namespace H_DataAccessLayer
{
    [Serializable]
    internal class BusException : Exception
    {
        public BusException()
        {
        }

        public BusException(string message) : base(message)
        {
        }

        public BusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}