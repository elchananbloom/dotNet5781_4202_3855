using System;
namespace dotNet5781_02_4202_3855

{
    public class BusLineStation : BusStation
    {
        private int busStationDistance; 
        private TimeSpan travelTime;
        //ctor
        public BusLineStation()
        {

        }
        public BusLineStation(int busStationDist, TimeSpan travelTime, int busStationKey, string stationAddress, double latitude, double longitude)
            :base(busStationKey, stationAddress, latitude, longitude)
        {
           
            this.busStationDistance = busStationDist;
            this.travelTime = travelTime;
        }
        //property.
        public int BusStationDist
        {
            get
            {
                return busStationDistance;
            }

            set
            {
                busStationDistance = value;
            }
        }
        public TimeSpan TravelTime
        {
            get { return travelTime; }
            set
            {
                travelTime = value;
            }
        }
        //All the class's functions.
        //the ToString() func as requested.
        public override string ToString()
        {
            string str = base.ToString() + "\n";
            str += "Bus Station Distance: " + BusStationDist + ", " + "Travel Time: " + TravelTime + ".";
            return str;
        }
    }
}