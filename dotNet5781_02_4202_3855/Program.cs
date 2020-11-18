using dotNet5781_02_4202_3855;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_02_4202_3855
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 0;
            string userChoice = string.Empty;
            List<BusLine> busesList = new List<BusLine>();
            BusLinesCollection busLines = new BusLinesCollection();
            Console.WriteLine("\tWelcome to the information system on bus lines and arrival times at stations.\n");
            Console.WriteLine("\t\t\tCreating 10 bus lines and 40 stations...\n");
            List<BusLineStation> stations = new List<BusLineStation>();
            int[] stationKeys = new int[40];
            string[] addres = { "Malissa", "Barton", "Heide", "Quintin", "Lula", "Ione", "Larhonda", "Yuko", "Wendolyn", "Willene", "Mitsue", "Ellsworth", "Angla", "Scarlet", "Xiao", "Khalilah", "Charlsie", "Norbert", "Ria", "Ladonna", "Jama", "Earlene", "Sylvia", "Ellie", "Loyce", "Meagan", "Oretha", "Corina", "Joelle", "Arthur", "Larita", "Dillon", "Beulah", "Fritz", "Danyelle", "Kerstin", "Deirdre", "Kathi", "Dodie", "Angelika" };
            Random rand = new Random();
            double latitude;
            double longitude;
            int busStationDist;
            TimeSpan travelTime;
            for (int i = 0; i < 40; i++)
            {
                stationKeys[i] = i + 25487 - 15 * i;
                latitude = NextDouble(rand, 31.0, 33.3);
                longitude = NextDouble(rand, 34.3, 35.5);
                busStationDist = rand.Next(500);
                travelTime = new TimeSpan(rand.Next(2), rand.Next(59), rand.Next(59));
                BusLineStation busSt = new BusLineStation(busStationDist, travelTime, stationKeys[i], addres[i], latitude, longitude);
                stations.Add(busSt);

            }
            Random randLineNum = new Random();//the line num
            string[] area = { "2", "3", "4", "1", "5", "1", "3", "2", "4", "5" };
            int index = 0;
            int anotherIndex = 0;
            int[] lineNumber = { randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550) };
            busLines.AddBusLine(new BusLine(lineNumber[anotherIndex], stations[index++], stations[index++], area[anotherIndex++]));
            busLines.AddBusLine(new BusLine(lineNumber[anotherIndex], stations[index++], stations[index++], area[anotherIndex++]));
            busLines.AddBusLine(new BusLine(lineNumber[anotherIndex], stations[index++], stations[index++], area[anotherIndex++]));
            busLines.AddBusLine(new BusLine(lineNumber[anotherIndex], stations[index++], stations[index++], area[anotherIndex++]));
            busLines.AddBusLine(new BusLine(lineNumber[anotherIndex], stations[index++], stations[index++], area[anotherIndex++]));
            busLines.AddBusLine(new BusLine(lineNumber[anotherIndex], stations[index++], stations[index++], area[anotherIndex++]));
            busLines.AddBusLine(new BusLine(lineNumber[anotherIndex], stations[index++], stations[index++], area[anotherIndex++]));
            busLines.AddBusLine(new BusLine(lineNumber[anotherIndex], stations[index++], stations[index++], area[anotherIndex++]));
            busLines.AddBusLine(new BusLine(lineNumber[anotherIndex], stations[index++], stations[index++], area[anotherIndex++]));
            busLines.AddBusLine(new BusLine(lineNumber[anotherIndex], stations[index++], stations[index++], area[anotherIndex++]));
            index = 10;
            int stationsIndex = 0;
            int lineNumIndex = 0;
            for (int i = 0; i < 9; i++)
            {
                busLines[lineNumber[lineNumIndex++], stations[stationsIndex]].AddStationToLineRoute(stations[index++], 1);
                stationsIndex += 2;
            }
            busLines[lineNumber[9], stations[stationsIndex]].AddStationToLineRoute(stations[9], 1);
            lineNumIndex = 0;
            stationsIndex = 0;
            for (int i = 0; i < 20; i++)
            {
                busLines[lineNumber[lineNumIndex++], stations[stationsIndex]].AddStationToLineRoute(stations[index++], 1);
                stationsIndex += 2;
                if (lineNumIndex == 10)
                {
                    lineNumIndex = 0;
                    stationsIndex = 0;
                }
            }
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
                //a bool varibale that will be used for all the convertions from string to int that we'll do during the code.
                bool success = true;
                if (success = Int32.TryParse(userChoice, out num))
                {
                    try
                    {
                        switch (num)
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
                                    PrintingOptions(busLines);
                                }
                                break;
                            case (int)Options.EXIT:
                                break;
                            default:
                                Console.WriteLine("You entered a wrong number. Please try again.");
                                break;
                        }
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.Message);
                    }
                }
            }
            while (num != 0);
        }
        //All the main's functions.

        //all the printing options.
        private static void PrintingOptions(BusLinesCollection busLines)
        {
            int choice = SubOptionsDisplays("Press:\n\t1: All bus lines in the system.\n\t2: List of all stations and line numbers passing through them.");
            switch (choice)
            {
                case (int)dotNet5781_02_4202_3855.PrintingOptions.ALL_BUS_LINES:

                    foreach (BusLine bus in busLines)
                    {
                        Console.WriteLine(bus);
                    }

                    break;
                case (int)dotNet5781_02_4202_3855.PrintingOptions.STATIONS_LIST_AND_BUSLINES:
                    busLines.PrintAllStationsAndLinesInStation();

                    break;
                default: break;
            }
        }
        //all the searching options.
        private static void SearchingOptions(BusLinesCollection busLines)
        {
            int choice = SubOptionsDisplays("Press:\n\t1: Lines passing through the station according to station number\n\t2: Printing the options for travel between 2 stations, without changing buses.");
            switch (choice)
            {
                case (int)dotNet5781_02_4202_3855.SearchingOptions.BUSSES_LINE:

                    Console.WriteLine("Enter the bus station's key.");
                    int busStationKey = int.Parse(Console.ReadLine());
                    List<BusLine> linesInStation = new List<BusLine>();
                    linesInStation = busLines.BusLinesInStation(busStationKey);
                    Console.WriteLine("\tPrinting all the lines passing through this station...");
                    foreach (BusLine bus in linesInStation)
                    {
                        Console.WriteLine(bus);
                    }
                    break;

                case (int)dotNet5781_02_4202_3855.SearchingOptions.OPTIONS_TRAVEL_BETWEEN_2_STATIONS:
                    Console.WriteLine("Enter the first station's details:");
                    BusLineStation departure = GetsStationDetails();
                    Console.WriteLine("Enter the second station's details:");
                    BusLineStation destination = GetsStationDetails();
                    BusLinesCollection sortedTravelOptions = new BusLinesCollection();
                    foreach (BusLine bus in busLines)
                    {
                        BusLine busss = bus.SubRoute(departure, destination, bus);
                        if (busss.BusLineNumber != 0)
                        {
                            sortedTravelOptions.AddBusLine(busss);
                        }
                    }
                    if(sortedTravelOptions.busLinesCollection.Count==0)
                    {
                        throw new KeyNotFoundException("There are no bus lines passing through those stations.");
                    }
                    sortedTravelOptions.SortBusLineTimes();
                    Console.WriteLine("\tPrinting all the lines passing between the stations...");
                    foreach (BusLine bus in sortedTravelOptions)
                    {
                        Console.WriteLine(bus);
                    }

                    break;
                default:
                    break;
            }
        }
        //all the deleting options.
        private static void DeletingOptions(BusLinesCollection busLines)
        {
            int choice = SubOptionsDisplays("Press:\n\t1: Subtraction of a bus line.\n\t2: Deletion of a station from a bus line route.");
            switch (choice)
            {
                case (int)dotNet5781_02_4202_3855.DeletingOptions.DELETE_BUSLINE:

                    int busLineNum;
                    Console.WriteLine("Enter the bus line number.");
                    busLineNum = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the first station of the bus.");
                    BusLineStation firstStationInBusLine = GetsStationDetails();
                    busLines.SubBusLine(busLines[busLineNum, firstStationInBusLine], firstStationInBusLine);
                    break;

                case (int)dotNet5781_02_4202_3855.DeletingOptions.DELETE_STATION_FROM_BUSLINE:

                    Console.WriteLine("Enter the bus line number.");
                    busLineNum = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the first station of the bus.");
                    firstStationInBusLine = GetsStationDetails();
                    Console.WriteLine("Enter the station you want to remove from the bus line route:");
                    BusLineStation removedStation = GetsStationDetails();

                    busLines[busLineNum, firstStationInBusLine].SubtractStationOfLineRoute(removedStation);

                    break;

                default:
                    break;
            }
        }
        //all the adding options.
        private static void AddOptions(BusLinesCollection busLines)
        {
            int busLineNumber;
            int choice = SubOptionsDisplays("Press:\n\t1: Adding a new bus line.\n\t2: Adding a stop to a bus line.");
            switch (choice)
            {
                case (int)dotNet5781_02_4202_3855.AddOptions.ADD_BUS:

                    Console.WriteLine("Enter the bus line number.");
                    int.TryParse(Console.ReadLine(), out busLineNumber);
                    Console.WriteLine("Enter the following first bus station's details:");
                    BusLineStation firstStation = GetsStationDetails();
                    Console.WriteLine("Enter the following last bus station's details:");
                    BusLineStation lastStation = GetsStationDetails();
                    Console.WriteLine("Enter the area where the bus runs:\n\t1: General.\n\t2: North.\n\t3: South.\n\t4: Center.\n\t5: Jerusalem.");
                    string area = Console.ReadLine();
                    BusLine newBus = new BusLine(busLineNumber, firstStation, lastStation, area);
                    busLines.AddBusLine(newBus);
                    break;

                case (int)dotNet5781_02_4202_3855.AddOptions.ADD_STATION_TO_BUSLINE:

                    Console.WriteLine("Enter the following new bus station's details:");
                    BusLineStation newBusStation = GetsStationDetails();
                    Console.WriteLine("Enter the bus line number.");
                    int.TryParse(Console.ReadLine(), out busLineNumber);
                    Console.WriteLine("Enter the following first bus station's details:");
                    firstStation = GetsStationDetails();
                    Console.WriteLine("Enter the place in the list to where you want to add the Bus Station");
                    int place = int.Parse(Console.ReadLine());
                    busLines[busLineNumber, firstStation].AddStationToLineRoute(newBusStation, place);
                    break;

                default:
                    break;
            }
        }
        //this func insert the bus station details.
        private static BusLineStation GetsStationDetails()
        {
            Console.WriteLine("Enter the bus station's key.");
            int busStationKey = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the bus station's address.");
            string stationAddress = Console.ReadLine();
            Random rand = new Random();
            double latitude = NextDouble(rand, 31.0, 33.3);
            double longitude = NextDouble(rand, 34.3, 35.5);
            int busStationDist = rand.Next(500);
            TimeSpan travelTime = new TimeSpan(rand.Next(2), rand.Next(59), rand.Next(59));
            BusLineStation newBus = new BusLineStation(busStationDist, travelTime, busStationKey, stationAddress, latitude, longitude);
            return newBus;
        }
        //this func is made for generating the random namber of the station's location, (since it is a double type range).
        static double NextDouble(Random rand, double minValue, double maxValue)
        {
            return rand.NextDouble() * (maxValue - minValue) + minValue;
        }
        //this functions displays (presents) the sub options displays to each case of the general switch and checks its correctness.
        private static int SubOptionsDisplays(string message)
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
