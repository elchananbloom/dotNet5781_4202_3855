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

    public enum Area { GENERAL = 1, NORTH, SOUTH, CENTER, JERUSALEM };
    class BusLine //:IComparable//קו אוטובוס בודד
    {
        private int busLineNumber;
        private BusLineStation firstStation;
        private BusLineStation lastStation;
        private string area;
        public List<BusLineStation> stationListHaloch = new List<BusLineStation>();//רשימת תחנות הלוך
        //properties.
        public int BusLineNumber { get => busLineNumber; set => busLineNumber = value; }
        internal BusLineStation FirstStation
        {
            get => firstStation;
            set
            {
                firstStation = value;
                stationListHaloch.Insert(0, FirstStation);
            }
        }
        internal BusLineStation LastStation
        {
            get => lastStation;
            set
            {
                lastStation = value;
                stationListHaloch.Add(lastStation);
            }
        }
        public string Area
        {
            get
            {
                string name = "";
                switch (int.Parse(area))
                {
                    case 1:
                        name = "General";
                        break;
                    case 2:
                        name = "North";
                        break;
                    case 3:
                        name = "South";
                        break;
                    case 4:
                        name = "Center";
                        break;
                    case 5:
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
        //the ToString() func as requested.
        public override string ToString()
        {
            string str = "Bus Number: " + busLineNumber + "," + "Area: " + area + "," + "bus station List:\n";
            for (int i = 0; i < stationListHaloch.Count; i++)
            {
                str += stationListHaloch[i].ToString();
                str += "\n";
            }
            //str += "Return station List:\n";
            //for (int i = 0; i < stationListReturn.Count; i++)
            //{
            //    str += stationListReturn[i].ToString();
            //    str += "\n";
            //}
            return str;
        }

        //הקריאה בפונקציה הראשית לפונקציה to string() 
        //BusStation busStation = new BusStation { busStationKey = ////, location };
        //Console.WriteLine(busStation);


        //this func adds a station to the line route.  
        public void AddStationToLineRoute(BusLineStation newBusStation)
        {
            // int num;
            //  string userChoice;
            // Console.WriteLine("Press:'/n' 1: To add the sation to the bus station list.");
            //  userChoice = Console.ReadLine();
            //  bool success = true;//a bool varibale that will be used for the convertion from string to int that we'll do in the nest line.
            //// if (success = Int32.TryParse(userChoice, out num))
            // {
            //switch (num)
            //{
            //    case (int)AddListOptions.ADD_TO_HALOCH:
            //        {

            bool result;
            result = stationChecking(newBusStation, stationListHaloch);
            Console.WriteLine("Enter the place in the list to where you want to add the Bus Station\nit needs to be between 0 - {0}.", stationListHaloch.Count + 1);
            int place = int.Parse(Console.ReadLine());
            if (result == false)
            {////////////////////////////////exception
                stationListHaloch.Insert(place, newBusStation);
                if (place == 0)
                {
                    firstStation = stationListHaloch[0];
                }
                if (place == stationListHaloch.Count - 1)
                {
                    lastStation = stationListHaloch[stationListHaloch.Count];
                }
                Console.WriteLine("The Bus Station has been added soccesfully at index {0}.", place);
                //int num1;
                //string Choice;
                //onsole.WriteLine("Press:'/n' 1: To add to the begging of the list.'/t' 2: To the middele of the list.'/t' 3: To the end of the list.");
                //Choice = Console.ReadLine();
                //bool prosper = true;//a bool varibale that will be used for the convertion from string to int that we'll do in the nest line.
                //if (prosper == Int32.TryParse(Choice, out num1))
                //{
                //    switch (num1)
                //    {
                //        case (int)Choices.ADD_TO_BEGGINING:
                //            {
                //                addBegin(newBusStation, stationListHaloch);
                //            }
                //            break;
                //        case (int)Choices.ADD_TO_MIDDELE:
                //            {
                //                Console.WriteLine("Enter the location index in the list.");
                //                int index = Int32.Parse(Console.ReadLine());
                //                stationListReturn.Insert(index, newBusStation);
                //            }
                //            break;
                //        case (int)Choices.ADD_TO_END:
                //            {
                //                stationListHaloch.Add(newBusStation);
                //            }
                //            break;
                //        default: break;
                //    }
                //}
            }
            else
                Console.WriteLine("The station you want to add already exists in the system.");
            // }
            //  break;

            //case (int)AddListOptions.ADD_TO_RETURN:
            //    {
            //        bool result;
            //        result = stationChecking(newBusStation, stationListReturn);
            //        Console.WriteLine("Enter the place in the list to where you want to add the Bus Station\nit needs to be between 0 - {0}.", stationListHaloch.Count + 1);
            //        int place = int.Parse(Console.ReadLine());
            //        if (result == false)
            //        {
            //            stationListReturn.Insert(place, newBusStation);
            //            Console.WriteLine("The Bus Station has been added soccesfully at index {0}.", place);

            //int num1;
            //string Choice;
            //onsole.WriteLine("Press:'/n' 1: To add to the begging of the list.'/t' 2: To the middele of the list.'/t' 3: To the end of the list.");
            //Choice = Console.ReadLine();
            //bool prosper = true;//a bool varibale that will be used for the convertion from string to int that we'll do in the nest line.
            //if (prosper == Int32.TryParse(Choice, out num1))
            //{
            //    switch (num1)
            //    {
            //        case (int)Choices.ADD_TO_BEGGINING:
            //            {
            //                addBegin(newBusStation, stationListHaloch);
            //            }
            //            break;
            //        case (int)Choices.ADD_TO_MIDDELE:
            //            {
            //                Console.WriteLine("Enter the location index in the list.");
            //                int index = Int32.Parse(Console.ReadLine());
            //                stationListReturn.Insert(index, newBusStation);
            //            }
            //            break;
            //        case (int)Choices.ADD_TO_END:
            //            {
            //                stationListHaloch.Add(newBusStation);
            //            }
            //            break;
            //        default: break;
            //    }
            //}
            //    }
            //    else
            //        Console.WriteLine("The station you want to add already exists in the system.");
            //}
            //    bool result;
            //    result = stationChecking(newBusStation, stationListHaloch);
            //    if (result == false)
            //    {
            //        int num1;
            //        string Choice;
            //        Console.WriteLine("Press:'/n' 1: To add to the begging of the list.'/t' 2: To the middele of the list.'/t' 3: To the end of the list.");
            //        Choice = Console.ReadLine();
            //        bool prosper = true;//a bool varibale that will be used for the convertion from string to int that we'll do in the nest line.
            //        if (prosper = Int32.TryParse(Choice, out num1))
            //        {
            //            switch (num1)
            //            {
            //                case (int)Choices.ADD_TO_BEGGINING:
            //                    {
            //                        addBegin(newBusStation, stationListReturn);
            //                    }
            //                    break;
            //                case (int)Choices.ADD_TO_MIDDELE:
            //                    {
            //                        Console.WriteLine("Enter the location index in the list.");
            //                        int index = Int32.Parse(Console.ReadLine());
            //                        stationListReturn.Insert(index, newBusStation);
            //                    }
            //                    break;
            //                case (int)Choices.ADD_TO_END:
            //                    {
            //                        stationListReturn.Add(newBusStation);
            //                    }
            //                    break;
            //                default: break;
            //            }
            //        }
            //    }
            //    else
            //        Console.WriteLine("The station you want to add already exists in the system.");
            //}
            //    break;
            //    default: break;
            //}
            //  }
        }

        //this func subtract a station from the line route.
        public void SubtractStationOfLineRoute(BusLineStation stationKey)
        {
            int num;
            string userChoice;
            Console.WriteLine("Press:\n\t 1: To remove the sation from the Haloch list.\n\t 2: To remove the station to the Return list.");
            userChoice = Console.ReadLine();
            bool success = true;
            if (success = Int32.TryParse(userChoice, out num))
            {
                bool result;
                result = stationChecking(stationKey, stationListHaloch);
                if (result)
                {
                    int place = find_index(stationKey, stationListHaloch);
                    stationListHaloch.RemoveAt(place);
                    if (place == 0)
                    {
                        firstStation = stationListHaloch[0];
                    }
                    if (place == stationListHaloch.Count + 1)
                    {
                        lastStation = stationListHaloch[stationListHaloch.Count];
                    }
                }
                else
                    Console.WriteLine("The station you want to remove does not exists in the list.");
            }
        }

        //this func checks if the station already exsists un the list by bus line station.
        public bool stationChecking(BusLineStation newBusStation, List<BusLineStation> stationsList)
        {
            return stationsList.Exists(s => s.BusStationKey == newBusStation.BusStationKey);
        }
        //this func checks if the station already exsists in the list by key number.
        public bool stationCheck(string number, List<BusLineStation> stationsList)
        {
            return stationsList.Exists(s => s.BusStationKey == number);

        }
        //this func returns the index of bus station in station list.
        public int find_index(BusLineStation newBusStation, List<BusLineStation> stationsList)
        {
            return stationsList.FindIndex(s => s.BusStationKey == newBusStation.BusStationKey);
        }

        //this fun calculates and returns the distance between 2 stations.
        public int stationsDistance(BusLineStation busStation1, BusLineStation busStation2)
        {
            int distance = 0;
            if (stationChecking(busStation1, stationListHaloch) && stationChecking(busStation2, stationListHaloch))
            {
                for (int i = find_index(busStation1, stationListHaloch) + 1; i < find_index(busStation2, stationListHaloch); i++)
                {
                    distance += stationListHaloch[i].BusStationDist;
                }
            }
            else
            {
                Console.WriteLine("The bus station you enterned does not exsists in this bus line route.");
            }
            return distance;
        }

        //this fun calculates and returns the travel time between 2 stations.
        public TimeSpan stationsTravelTime(BusLineStation busStation1, BusLineStation busStation2)
        {
            TimeSpan travelTime = new TimeSpan(0, 0, 0);
            if (stationChecking(busStation1, stationListHaloch) && stationChecking(busStation2, stationListHaloch))
            {
                for (int i = find_index(busStation1, stationListHaloch); i < find_index(busStation2, stationListHaloch); i++)
                {
                    travelTime += stationListHaloch[i].TravelTime;
                }
            }
            else
            {
                Console.WriteLine("The bus station you enterned does not exsists in this bus line route.");
            }
            return travelTime;
        }

        //this func return the sub route between 2 stations
        public BusLine subRoute(BusLineStation busStation1, BusLineStation busStation2)
        {
            BusLine subRoute = new BusLine();
            //  List<BusLineStation> subRoute = new List<BusLineStation>();
            if (stationChecking(busStation1, stationListHaloch) && stationChecking(busStation2, stationListHaloch))
            {
                for (int i = find_index(busStation1, stationListHaloch); i < find_index(busStation2, stationListHaloch); i++)
                {
                    subRoute.stationListHaloch.Add(stationListHaloch[i]);
                }
            }
            return subRoute;
        }

        //this func compare the travel time of two bus lines.
        public int compareTwoBusLines(BusLine line)
        {
            // if (stationChecking(busStation1, stationListHaloch) && stationChecking(busStation2, stationListHaloch) && stationChecking(busStation1, a.stationListHaloch) && stationChecking(busStation2, a.stationListHaloch))
            // {
            return this.CompareTo(line);
            // }
        }
        //this funp overrides the original CompareTo function.
        public int CompareTo(object obj)
        {
            TimeSpan time1 = stationsTravelTime(firstStation, lastStation);
            TimeSpan time2 = stationsTravelTime(((BusLine)obj).firstStation, ((BusLine)obj).lastStation);
            return time1.CompareTo(time2);
            //   throw new NotImplementedException();
        }

    }

}