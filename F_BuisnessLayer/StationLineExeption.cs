using System;
using System.Runtime.Serialization;

namespace BuisnessLayer
{
    [Serializable]
    internal class StationLineExeption : Exception
    {
        public StationLineExeption()
        {
        }

        public StationLineExeption(string message) : base(message)
        {
        }

        public StationLineExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StationLineExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}