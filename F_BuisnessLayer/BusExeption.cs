using System;
using System.Runtime.Serialization;

namespace F_BuisnessLayer
{
    [Serializable]
    internal class BusExeption : Exception
    {
        public BusExeption()
        {
        }

        public BusExeption(string message) : base(message)
        {
        }

        public BusExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}