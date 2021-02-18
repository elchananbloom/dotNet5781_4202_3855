using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    internal class CoupleStationException : Exception
    {
        public CoupleStationException()
        {
        }

        public CoupleStationException(string message) : base(message)
        {
        }

        public CoupleStationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CoupleStationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}