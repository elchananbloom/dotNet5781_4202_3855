﻿using System;
using System.Runtime.Serialization;

namespace BuisnessLayer
{
    [Serializable]
    internal class StationExeption : Exception
    {
        public StationExeption()
        {
        }

        public StationExeption(string message) : base(message)
        {
        }

        public StationExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StationExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}