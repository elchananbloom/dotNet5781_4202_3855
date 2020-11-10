﻿using dotNet5781_02_4202_3855;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * indexer
 * IEnumerable
 * Exception
 * 
 */
namespace dotNet_02_4202_3855
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 0;
            string userChoice = string.Empty;
            List<BusLine> busesList = new List<BusLine>();
            BusLinesCollection busLines = new BusLinesCollection();
            Console.WriteLine("   Welcome to the information system on bus lines and arrival times at stations.");
            do
            {
                Console.WriteLine("\nSelect the number of your choice:");
                Console.WriteLine("\t1: Adding options");
                Console.WriteLine("\t2: Deleting options");
                Console.WriteLine("\t3: Searching options");
                Console.WriteLine("\t4: Printing options");
                Console.WriteLine("\t0: Exit");
                Console.WriteLine("Enter the number of your choice: ");
                userChoice = Console.ReadLine();

                //List<string> busStationNames = { "Amarpe", "Golda", "Kikar Hashabat", "Binyane Ahoma", "Givat Mordechai", "Masof Egged", "Merkaz Arav", "Shachal", "Tedi", "Chazon ish", "Tzomet Ramot",
                //"Yrmiyaho", "Central Bus Station", "Kanfe Nesharim", "Beth Adfos", "Mintz", "Tzondak", "Machane Yahouda", "Aza", "Shabtay Anegbi", "Yefe Rom", "Hertzog", "Ahoman" };
                //for (int i=0; i<10; i++)
                //{
                //    BusLine busline = new BusLine(i*7-3*i+5);


                //}

                bool success = true;//a bool varibale that will be used for all the convertions from string to int that we'll do during the code.
                if (success = Int32.TryParse(userChoice, out num))
                {
                    //from here on we have the entire program.
                    switch (num)//a switch for the menu options.
                    {
                        case (int)Options.ADD:
                            {
                                AddOptions(busLines);
                            }
                            break;

                        case (int)Options.DELETE:
                            {
                                DeletingOptions(busLines);
                            }
                            break;

                        case (int)Options.SEARCH:
                            {
                                    SearchingOptions(busLines);
                            }
                            break;

                        case (int)Options.PRINT:
                            {
                                //printingOptions();//////
                                {
                                    int choice = subOptionsDisplays("Press:\n     1: All bus lines in the system.\n     2: List of all stations and line numbers passing through them.");

                                    switch (choice)
                                    {
                                        case (int)PrintingOptions.ALL_BUS_LINES:

                                            foreach(BusLine bus in busLines)
                                            {
                                                Console.WriteLine(bus);
                                            }

                                            break;
                                        case (int)PrintingOptions.STATIONS_LIST_AND_BUSLINES:



                                            break;
                                        default: break;
                                    }


                                }


                            }
                            break;
                        case (int)Options.EXIT:
                            break;
                        default:
                            Console.WriteLine("You entered a wrong number. Please try again.");
                            break;
                    }
                }
            }
            while (num != 0);










        }

        private static void SearchingOptions(BusLinesCollection busLines)
        {
            int choice = subOptionsDisplays("Press:\n     1: Lines passing through the station according to station number\n     2: Printing the options for travel between 2 stations, without changing buses.");

            switch (choice)
            {
                case (int)dotNet5781_02_4202_3855.SearchingOptions.BUSSES_LINE:

                    string busStationKey = Console.ReadLine();
                    List<BusLine> linesInStation = new List<BusLine>();
                    linesInStation = busLines.busLinesInStation(busStationKey);

                    break;

                case (int)dotNet5781_02_4202_3855.SearchingOptions.OPTIONS_TRAVEL_BETWEEN_2_STATIONS:

                    BusLineStation departure = new BusLineStation();
                    getsStationDetails(departure);
                    BusLineStation destination = new BusLineStation();
                    getsStationDetails(destination);
                    BusLinesCollection sortedTravelOptions = new BusLinesCollection();
                    foreach (BusLine bus in busLines)
                    {
                        sortedTravelOptions.addBusLine(bus.subRoute(departure, destination));
                    }
                    sortedTravelOptions.sortBusLineTimes();
                    foreach (BusLine bus in sortedTravelOptions)
                    {
                        Console.WriteLine(bus);
                    }

                    break;
                default:
                    break;
            }
        }

        private static void DeletingOptions(BusLinesCollection busLines)
        {
            int choice = subOptionsDisplays("Press:\n\t1: Subtraction of a bus line.\n\t2: Deletion of a station from a bus line route.");

            switch (choice)
            {
                case (int)dotNet5781_02_4202_3855.DeletingOptions.DELETE_BUSLINE:
                    int busLineNum;
                    BusLineStation firstStationInBusLine;
                    Console.WriteLine("Enter the bus line number.");
                    busLineNum = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the first station of the bus.");
                    firstStationInBusLine = new BusLineStation();
                    getsStationDetails(firstStationInBusLine);
                    busLines.subBusLine(busLines[busLineNum, firstStationInBusLine], firstStationInBusLine);
                    break;

                case (int)dotNet5781_02_4202_3855.DeletingOptions.DELETE_STATION_FROM_BUSLINE:

                    Console.WriteLine("Enter the bus line number.");
                    busLineNum = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the first station of the bus.");
                    firstStationInBusLine = new BusLineStation();
                    getsStationDetails(firstStationInBusLine);
                    Console.WriteLine("Enter the station you want to remove from the bus line route.");
                    BusLineStation removedStation = new BusLineStation();
                    getsStationDetails(removedStation);
                    busLines[busLineNum, firstStationInBusLine].SubtractStationOfLineRoute(removedStation);

                    break;

                default:
                    break;
            }
        }

        private static void AddOptions(BusLinesCollection busLines)
        {
            int busLineNumber;
            BusLine newBus = new BusLine();
            int choice = subOptionsDisplays("Press:\n     1: Adding a new bus line.\n     2: Adding a stop to a bus line.");
            switch (choice)
            {
                case (int)dotNet5781_02_4202_3855.AddOptions.ADD_BUS:

                    Console.WriteLine("Enter the bus line number.");
                    int.TryParse(Console.ReadLine(), out busLineNumber);
                    newBus.BusLineNumber = busLineNumber;
                    Console.WriteLine("Enter the following first bus station's details:");
                    getsStationDetails(newBus.FirstStation);
                    Console.WriteLine("Enter the following last bus station's details:");
                    getsStationDetails(newBus.LastStation);
                    Console.WriteLine("Enter the area where the bus runs:\n\t1: General.\n\t2: North.\n\t3: South.\n\t4: Center.\n\t5: Jerusalem.");
                    newBus.Area = Console.ReadLine();
                    busLines.addBusLine(newBus);
                    break;

                case (int)dotNet5781_02_4202_3855.AddOptions.ADD_STATION_TO_BUSLINE:

                    BusLineStation newBusStation = new BusLineStation();
                    Console.WriteLine("Enter the following bus station's details:");
                    getsStationDetails(newBusStation);
                    Console.WriteLine("Enter the bus line number.");
                    int.TryParse(Console.ReadLine(), out busLineNumber);
                    Console.WriteLine("Enter the following first bus station's details:");
                    getsStationDetails(newBus.FirstStation);
                    busLines[busLineNumber, newBus.FirstStation].AddStationToLineRoute(newBusStation);
                    //find specific bus line - keep it 
                    ///////////////////////////////////////////////////////indexer
                    // busesList[0].AddStationToLineRoute(newBusStation);
                    break;

                default:
                    break;
            }
        }

        //this func insert the bus station details.
        private static void getsStationDetails(BusLineStation newBus)
        {
            Console.WriteLine("Enter the bus station's key.");
            newBus.BusStationKey = Console.ReadLine();
            Console.WriteLine("Enter the bus station's address.");
            newBus.StationAddress = Console.ReadLine();
            Random rand = new Random();
            newBus.Latitude = NextDouble(rand, 31.0, 33.3);
            newBus.Longitude = NextDouble(rand, 34.3, 35.5);
            newBus.BusStationDist = rand.Next(500);
            newBus.TravelTime = new TimeSpan(rand.Next(2), rand.Next(59), rand.Next(59));
        }
        //this func is made for generating the random namber of the station's location, (since it is a double type range).
        ////////////////////////////////main
        static double NextDouble(Random rand, double minValue, double maxValue)
        {
            return rand.NextDouble() * (maxValue - minValue) + minValue;
        }


        //All the main's functions.

        //this functions displays (presents) the sub options displays to each case of the general switch.
        private static int subOptionsDisplays(string message)
        {
            int choice = 0;
            do
            {
                Console.WriteLine(message);
                string option = Console.ReadLine();
                Int32.TryParse(option, out choice);
            }
            while (choice != 1 && choice != 2);
            return choice;
        }
    }
}
