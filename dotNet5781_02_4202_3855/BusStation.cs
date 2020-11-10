using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace dotNet_02_4202_3855
{
    class BusStation//תחנת אוטובוס
    {
        private const int MIN_LATITUDE = -90;
        private const int MAX_LATITUDE = 90;
        private const int MIN_LONGITUDE = -180;
        private const int MAX_LONGITUDE = 180;

        protected string busStationKey;//the station's code.
        protected string stationAddress;//the address of the bus station.
        protected double latitude;
        protected double longitude;
        private List<string> serials = new List<string>();

        //properties
        public string BusStationKey
        {
            get { return busStationKey; }
            set
            {
                if (serials.Contains(value))
                {
                    throw new ArgumentException(
                        String.Format("{0} key number exists allready", value));
                }
                if (value.Length > 6)
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
                }                //Random rand = new Random();
                                 //double result;
                                 //do
                                 //{
                                 //    result = NextDouble(rand, 31, 33.3);
                                 //}
                                 //while (result < -90 || result > 90);
                                 // latitude = result;
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
                //Random rand = new Random();
                //double result;
                //do
                //{
                //    result = NextDouble(rand, 34.3, 35.5);
                //}
                //while (result < -180 || result > 180);
                //longitude = result;
            }
        }
        // All the class's functions.
       
        //the ToString() func as requested.
        public override string ToString()
        {
            String result = "Bus Station Code: " + BusStationKey + "Bus Station Address" + stationAddress;
            result += String.Format(", {0}°{1} {2}°{3}",
                Math.Abs(Latitude), (Latitude > 0) ? "N" : "S",
                Math.Abs(Longitude), (Longitude > 0) ? "E" : "W");
            return result;
           // return "Bus Station Code: " + BusStationKey + ", " + Latitude + "°N " + Longitude + "°E";
        }

        //הקריאה בפונקציה הראשית לפונקציה to string() 
        //BusStation busStation = new BusStation { busStationKey = ////, location };
        //Console.WriteLine(busStation);







    }


}