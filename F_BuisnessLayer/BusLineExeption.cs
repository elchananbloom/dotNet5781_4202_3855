using System;
using System.Runtime.Serialization;

namespace BuisnessLayer
{
    [Serializable]
    internal class BusLineExeption : Exception
    {
        public BusLineExeption()
        {
        }

        public BusLineExeption(string message) : base(message)
        {
        }

        public BusLineExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusLineExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}