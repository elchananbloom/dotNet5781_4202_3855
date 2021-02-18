using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    internal class StationLineException : Exception
    {
        public StationLineException()
        {
        }

        public StationLineException(string message) : base(message)
        {
        }

        public StationLineException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StationLineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}