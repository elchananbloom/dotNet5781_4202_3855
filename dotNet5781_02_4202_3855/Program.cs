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

                //List<string> busStationNames = { "Amarpe", "Golda", "Kikar Hashabat", "Binyane Ahoma", "Givat Mordechai", "Masof Egged", "Merkaz Arav", "Shachal", "Tedi", "Chazon ish", "Tzomet Ramot", "Yrmiyaho", "Central Bus Station", "Kanfe Nesharim", "Beth Adfos", "Mintz", "Tzondak", "Machane Yahouda", "Aza", "Shabtay Anegbi", "Yefe Rom", "Hertzog", "Ahoman" };
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
                                //addOptions(ref success);////////
                                //{
                                int choice = subOptionsDisplays("Press:\n     1: Adding a new bus line.\n     2: Adding a stop to a bus line.");
                                switch (choice)
                                {
                                    case 1:
                                        BusLine newBus = new BusLine();
                                        Console.WriteLine("Enter the bus line number.");
                                        int busLineNumber;
                                        int.TryParse(Console.ReadLine(), out busLineNumber);
                                        newBus.BusLineNumber = busLineNumber;
                                        Console.WriteLine("Enter the following first bus station's details:");
                                        getsStationDetails(newBus.FirstStation);
                                        Console.WriteLine("Enter the following last bus station's details:");
                                        getsStationDetails(newBus.LastStation);
                                        Console.WriteLine("Enter the area where the bus runs:");
                                        newBus.Area = Console.ReadLine();
                                        busLines.addBusLine(newBus);


                                        break;
                                    case 2:
                                        BusLineStation newBusStation = new BusLineStation();
                                        //Console.WriteLine("2. Enter the bus station's latitude.'/n'");
                                        //newBusStation.Latitude = Double.Parse(Console.ReadLine());
                                        //Console.WriteLine("2. Enter the bus station's longitude.'/n'");
                                        //newBusStation.Longitude = Double.Parse(Console.ReadLine());
                                        Console.WriteLine("Enter the bus line number.");
                                        //int busLineNumber;
                                        int.TryParse(Console.ReadLine(), out busLineNumber);
                                        //find specific bus line - keep it 
                                        ///////////////////////////////////////////////////////indexer
                                        // busesList[0].AddStationToLineRoute(newBusStation);

                                        break;
                                    default: break;
                                }
                                //}
                            }
                            break;

                        case (int)Options.DELETE:
                            {
                                // deletingOptions();////////
                                {
                                    int choice = subOptionsDisplays("Press:\n     1: Subtraction of a bus line.\n     2: Deletion of a station from a bus line route.");

                                    switch (choice)
                                    {
                                        case 1:



                                            break;
                                        case 2:
                                            string StationKey;
                                            Console.WriteLine("Enter the bus station's key of the station you want to remove from the bus line           route.'/n'");
                                            StationKey = Console.ReadLine();
                                            SubtractStationOfLineRoute(stationKey);
                                            break;
                                        default: break;
                                    }
                                }
                            }
                            break;

                        case (int)Options.SEARCH:
                            {
                                searchingOptions();///////
                                {
                                    int choice = subOptionsDisplays("Press:\n     1: Lines passing through the station according to station number\n     2: Printing the options for travel between 2 stations, without changing buses.");

                                    switch (choice)
                                    {
                                        case 1:



                                            break;
                                        case 2:



                                            break;
                                        default: break;
                                    }

                                }

                            }
                            break;

                        case (int)Options.PRINT:
                            {
                                printingOptions();//////
                                {
                                    int choice = subOptionsDisplays("Press:\n     1: All bus lines in the system.\n     2: List of all stations and line numbers passing through them.");

                                    switch (choice)
                                    {
                                        case 1:



                                            break;
                                        case 2:



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

        private static void getsStationDetails(BusLineStation newBus)
        {
            Console.WriteLine("Enter the bus station's key.");
            newBus.BusStationKey = Console.ReadLine();
            Console.WriteLine("Enter the bus station's address.");
            newBus.StationAddress = Console.ReadLine();
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
