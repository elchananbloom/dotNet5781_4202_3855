using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace dotNet5781_02_4202_3855
{
    public class BusStation
    {
        private const int MAX_DIGITS = 1000000;
        private const int MIN_LATITUDE = -90;
        private const int MAX_LATITUDE = 90;
        private const int MIN_LONGITUDE = -180;
        private const int MAX_LONGITUDE = 180;

        protected int busStationKey;
        protected string stationAddress;
        protected double latitude;
        protected double longitude;
        private List<int> serials = new List<int>();
        //ctor
        public BusStation(int busStationKey, string stationAddress, double latitude, double longitude)
        {
            this.BusStationKey = busStationKey;
            this.StationAddress = stationAddress;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
        //operators
        public static bool operator ==(BusStation buss, BusStation b)
        {
            return buss.busStationKey == b.busStationKey;
        }
        public static bool operator !=(BusStation buss, BusStation b)
        {
            return buss.busStationKey != b.busStationKey;
        }
        //properties
        public int BusStationKey
        {
            get { return busStationKey; }
            set
            {
                if (serials.Contains(value))
                {
                    throw new ArgumentException(
                        String.Format("{0} key number exists allready", value));
                }
                if (value <= 0 || value >= MAX_DIGITS)
                {
                    throw new ArgumentException(
                                           String.Format("{0} is not a valid key number", value));
                }
                busStationKey = value;
                serials.Add(BusStationKey);
            }
        }
        public string StationAddress
        {
            get { return stationAddress; }
            set { stationAddress = value; }
        }
        public double Latitude
        {
            get { return latitude; }
            set
            {
                if (value >= MIN_LATITUDE && value <= MAX_LATITUDE)
                {
                    latitude = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Latitude",
                        String.Format("{0} should be between {1} and {2}", value, MIN_LATITUDE, MAX_LATITUDE));
                }               
            }
        }
        public double Longitude
        {
            get { return longitude; }
            set
            {
                if (value >= MIN_LONGITUDE && value <= MAX_LONGITUDE)
                {
                    longitude = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Longitude",
                        String.Format("{0} should be between {1} and {2}", value, MIN_LONGITUDE, MAX_LONGITUDE));
                }
            }
        }
        // All the class's functions.
       
        //the ToString() func as requested.
        public override string ToString()
        {
            String result = "\n\t\tBus Station Code: " + BusStationKey + "\n\t\tBus Station Address: " + stationAddress;
            result += String.Format("\n\t\tLatitude: {0}°{1}, Longitude: {2}°{3}",
                Math.Abs(Latitude), (Latitude > 0) ? "N" : "S",
                Math.Abs(Longitude), (Longitude > 0) ? "E" : "W");
            return result;
        }
    }
}