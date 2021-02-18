using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    internal class BusException : Exception
    {
        private object p;

        public BusException()
        {
        }

        public BusException(object p)
        {
            this.p = p;
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