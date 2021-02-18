using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    internal class LineInServiceException : Exception
    {
        public LineInServiceException()
        {
        }

        public LineInServiceException(string message) : base(message)
        {
        }

        public LineInServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LineInServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}