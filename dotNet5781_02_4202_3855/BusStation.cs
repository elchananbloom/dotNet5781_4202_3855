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
        protected string busStationKey;//the station's code.
        protected string stationAddress;//the address of the bus station.
        protected double latitude;
        protected double longitude;


        //properties
        public string BusStationKey
        {
            get { return busStationKey; }
            set
            {
                if (value.Length > 6)
                {
                    Console.WriteLine("Error, incorrect bus station number. Please enter a number that is less than 6 digits.");//////can do an exception instead.
                }
                else
                {
                    busStationKey = value;
                }
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
                Random rand = new Random();
                double result;
                do
                {
                    result = NextDouble(rand, 31, 33.3);
                }
                while (result < -90 || result > 90);
                latitude = result;
            }
        }
        public double Longitude
        {
            get { return longitude; }
            set
            {
                Random rand = new Random();
                double result;
                do
                {
                    result = NextDouble(rand, 34.3, 35.5);
                }
                while (result < -180 || result > 180);
                longitude = result;
            }
        }
        // All the class's functions.
        //this func is made for generating the random namber of the station's location, (since it is a double type range).
        public double NextDouble(Random rand, double minValue, double maxValue)
        {
            return rand.NextDouble() * (maxValue - minValue) + minValue;
        }
        //the ToString() func as requested.
        public override string ToString()
        {
            return "Bus Station Code: " + BusStationKey + ", " + Latitude + "°N " + Longitude + "°E";
        }

        //הקריאה בפונקציה הראשית לפונקציה to string() 
        //BusStation busStation = new BusStation { busStationKey = ////, location };
        //Console.WriteLine(busStation);







    }


}