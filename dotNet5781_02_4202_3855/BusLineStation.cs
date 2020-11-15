using System;
namespace dotNet_02_4202_3855

{
    class BusLineStation : BusStation//תחנת קו אוטובוס
    {
        private int busStationDist;//מרחק מתחנת קו אוטובוס הקודמת 
        private TimeSpan travelTime;//זמן נסיעה מתחנת קו אוטובוס הקודמת 

        public BusLineStation(int busStationDist, TimeSpan travelTime, int busStationKey, string stationAddress, double latitude, double longitude) :base(busStationKey, stationAddress, latitude, longitude)
        {
           
            this.busStationDist = busStationDist;
            this.travelTime = travelTime;
        }

        //property.
        public int BusStationDist
        {
            get
            {
                return busStationDist;
            }

            set
            {
                //Random rand = new Random();
                //busStationDist = rand.Next(500);
                busStationDist = value;
            }
        }
        public TimeSpan TravelTime
        {
            get { return travelTime; }
            set
            {
                //Random rand = new Random();
                //travelTime = new TimeSpan(rand.Next(2), rand.Next(59), rand.Next(59));
                travelTime = value;
            }
        }
        //All the class's functions.
        //the ToString() func as requested.
        public override string ToString()
        {////////////////
            string str = base.ToString() + "\n";
            str += "\t\tBusStationDist: " + BusStationDist + ", " + "TravelTime: " + TravelTime + ".\n";
            return str;
        }
        //public static BusLineStation operator=(BusLineStation a,BusLineStation b)
        //{

        //}



    }

}