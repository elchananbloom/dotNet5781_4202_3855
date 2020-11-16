using dotNet5781_02_4202_3855;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace dotNet_02_4202_3855
{
    class BusLine
    {
        private int busLineNumber;
        private BusLineStation firstStation;
        private BusLineStation lastStation;
        private string area;
        public List<BusLineStation> stationList = new List<BusLineStation>();
        //ctors.
        public BusLine(int busLineNumber, BusLineStation firstStation, BusLineStation lastStation, string area)
        {
            this.busLineNumber = busLineNumber;
            this.firstStation = firstStation;
            this.lastStation = lastStation;
            this.area = area;
            stationList.Insert(0, firstStation);
            stationList.Add(lastStation);
        }
        public BusLine()
        {
        }
        //properties.
        public int BusLineNumber { get => busLineNumber; set => busLineNumber = value; }
        public BusLineStation FirstStation
        {
            get => firstStation;
            set
            {
                firstStation = value;
                stationList.Insert(0, FirstStation);
            }
        }
        public BusLineStation LastStation
        {
            get => lastStation;
            set
            {
                lastStation = value;
                stationList.Add(lastStation);
            }
        }
        public string Area
        {
            get
            {
                string name = "";
                switch (int.Parse(area))
                {
                    case (int)Areas.GENERAL:
                        name = "General";
                        break;
                    case (int)Areas.NORTH:
                        name = "North";
                        break;
                    case (int)Areas.SOUTH:
                        name = "South";
                        break;
                    case (int)Areas.CENTER:
                        name = "Center";
                        break;
                    case (int)Areas.JERUSALEM:
                        name = "Jerusalem";
                        break;
                    default:
                        break;
                }
                return name;
            }
            set => area = value;
        }
        //All the class's functions.

        //this func adds a station to the line route.  
        public void AddStationToLineRoute(BusLineStation newBusStation,int place)
        {
            bool result;
            result = StationChecking(newBusStation, stationList);
            if (result == false)
            {
                if (place > stationList.Count)
                {
                    throw new IndexOutOfRangeException("The index is out of range.");
                }
                stationList.Insert(place, newBusStation);
                if (place == 0)
                {
                    firstStation = stationList[0];
                }
                if (place == stationList.Count)
                {
                    lastStation = stationList[stationList.Count];
                }
                Console.WriteLine("The Bus Station has been added soccesfully at index {0}.", place);
            }
            else
                throw new KeyNotFoundException("The station does not exists.");
        }
        //this func subtract a station from the line route.
        public void SubtractStationOfLineRoute(BusLineStation stationKey)
        {
                bool result;
                result = StationChecking(stationKey, stationList);
                if (result)
                {
                    int place = Find_index(stationKey, stationList);
                    stationList.RemoveAt(place);
                    if (place == 0)
                    {
                        firstStation = stationList[0];
                    }
                    if (place == stationList.Count)
                    {
                        lastStation = stationList[stationList.Count-1];
                    }
                    Console.WriteLine("The station has been removed successfully.");
                }
                else
                    throw new KeyNotFoundException("The station does not exists.");
        }
        //this func checks if the station already exsists in the list by bus line station.
        public bool StationChecking(BusLineStation newBusStation, List<BusLineStation> stationsList)
        {
            return stationsList.Exists(s => s.BusStationKey == newBusStation.BusStationKey);
        }
        //this func checks if the station already exsists in the list by key number.
        public bool StationCheck(int key, List<BusLineStation> stationsList)
        {
            return stationsList.Exists(s => s.BusStationKey == key);
        }
        //this func returns the index of bus station in station list.
        public int Find_index(BusLineStation newBusStation, List<BusLineStation> stationsList)
        {
            return stationsList.FindIndex(s => s.BusStationKey == newBusStation.BusStationKey);
        }
        //this fun calculates and returns the distance between 2 stations.
        public int StationsDistance(BusLineStation busStation1, BusLineStation busStation2)
        {
            int distance = 0;
            if (StationChecking(busStation1, stationList) && StationChecking(busStation2, stationList))
            {
                for (int i = Find_index(busStation1, stationList) + 1; i < Find_index(busStation2, stationList); i++)
                {
                    distance += stationList[i].BusStationDist;
                }
            }
            return distance;
        }
        //this fun calculates and returns the travel time between 2 stations.
        public TimeSpan StationsTravelTime(BusLineStation busStation1, BusLineStation busStation2)
        {
            TimeSpan travelTime = new TimeSpan(0, 0, 0);
            if (StationChecking(busStation1, stationList) && StationChecking(busStation2, stationList))
            {
                for (int i = Find_index(busStation1, stationList); i < Find_index(busStation2, stationList); i++)
                {
                    travelTime += stationList[i].TravelTime;
                }
            }
            return travelTime;
        }
        //this func return the sub route between 2 stations
        public BusLine SubRoute(BusLineStation busStation1, BusLineStation busStation2,BusLine bus)
        {
            BusLine subRoute = new BusLine();
            if (StationChecking(busStation1, stationList) && StationChecking(busStation2, stationList))
            {
                subRoute.busLineNumber = bus.busLineNumber;
                subRoute.area = bus.area;
                for (int i = Find_index(busStation1, stationList); i < Find_index(busStation2, stationList)+1; i++)
                {
                    subRoute.stationList.Add(stationList[i]);
                }
            }
            return subRoute;
        }
        //this func compare the travel time of two bus lines.
        public int CompareTwoBusLines(BusLine line)
        {
            return this.CompareTo(line);
        }
        //this funp overrides the original CompareTo function.
        public int CompareTo(object obj)
        {
            TimeSpan time1 = StationsTravelTime(firstStation, lastStation);
            TimeSpan time2 = StationsTravelTime(((BusLine)obj).firstStation, ((BusLine)obj).lastStation);
            return time1.CompareTo(time2);
        }
        //the ToString() func as requested.
        public override string ToString()
        {
            string str = "Bus Number: " + busLineNumber + ".\n\tArea: " + Area + ".\n\tBus stations List:\n";
            for (int i = 0; i < stationList.Count; i++)
            {
                str += stationList[i].ToString();
                str += "\n";
            }
            return str;
        }
    }
}