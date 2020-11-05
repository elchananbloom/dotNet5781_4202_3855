using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_01_4202_3855
{
    public enum Menu { EXIT, ADD_BUS, CHOOSE_BUS, REFUELING_OR_HANDLING_BUS, TRAVELED }//an enum for the menu for the user.
    public enum Options { REFUELING = 1, HANDLING }//an enum for the options for the case of "RefuelingOrHandlingBus" who is in the menu.

    class Program
    {
        static List<Bus> buses = new List<Bus>();//Declaration of the bus list.
        static void Main(string[] args)
        {
            int num = 0;
            string userChoice = string.Empty;
            Console.WriteLine("   Welcome to the bus system.");
            do
            {
                Console.WriteLine("\nSelect the number of your choice:");
                Console.WriteLine("\t1: Add a bus");
                Console.WriteLine("\t2: Choose a bus");
                Console.WriteLine("\t3: Performe refueling or handling of a bus");
                Console.WriteLine("\t4: Travel presentation");
                Console.WriteLine("\t0: Exit");
                Console.WriteLine("Enter the number of your choice: ");
                userChoice = Console.ReadLine();

                bool success = true;//a bool varibale that will be used for all the convertions from string to int that we'll do during the code.
                if (success = Int32.TryParse(userChoice, out num))
                {
                    //from here on we have the entire program.
                    switch (num)//a switch for the menu options.
                    {
                        case (int)Menu.ADD_BUS:
                            {
                                addBus(ref success);
                            }
                            break;
                        case (int)Menu.CHOOSE_BUS:
                            {
                                chooseBus();
                            }
                            break;
                        case (int)Menu.REFUELING_OR_HANDLING_BUS:
                            {
                                refuelingOrHandling();
                            }
                            break;
                        case (int)Menu.TRAVELED:
                            {
                                printAll();
                            }
                            break;
                        case (int)Menu.EXIT:
                            break;
                        default:
                            Console.WriteLine("You entered a wrong number. Please try again.");
                            break;
                    }
                }
            }
            while (num != 0);//it will show again the menu to the user after each case (of the switch) again and again, until the the user input the number 0 (for Exit).
            return;
            //end of program   
        }
        //All the functions of the program.

        private static void printAll()//the func prints all the buses's data
        {
            int i = 1;
            foreach (Bus bus in buses)
            {
                Console.WriteLine("Bus number: {0}.", i++);
                Console.WriteLine("\tLicense number: {0}.", printLicenseNum(bus));
                Console.WriteLine("\tCommencement of activity: {0}.", bus.DateBegin);
                Console.WriteLine("\tTotal mileage: {0}.", bus.TotalMileage);
                Console.WriteLine("\tLast maintenance day: {0}.", bus.LastTreatment);
                Console.WriteLine("\tKilometer driven since last maintenance: {0}.", bus.Maintenance);
                Console.WriteLine("\tKilometers left from last fueling: {0}.\n", bus.FuelStatus);
            }
        }

        private static string printLicenseNum(Bus bus)//the func prints the bus's license number
        {
            string result;
            if (bus.DateBegin.Year < 2018)
            {
                string begin = bus.LicenseNumber.Substring(0, 2);
                string middle = bus.LicenseNumber.Substring(2, 3);
                string end = bus.LicenseNumber.Substring(5, 2);
                result = String.Format("{0}-{1}-{2}", begin, middle, end);
            }
            else
            {
                string begin = bus.LicenseNumber.Substring(0, 3);
                string middle = bus.LicenseNumber.Substring(3, 2);
                string end = bus.LicenseNumber.Substring(5, 3);
                result = String.Format("{0}-{1}-{2}.", begin, middle, end);
            }

            return result;
        }

        private static void refuelingOrHandling()//this func does refueling and handling
        {
            bool check = false;
            do
            {
                string licenceNumber = getLicenseNumber();
                foreach (Bus bus in buses)//check if the bus exists
                {
                    if (bus.LicenseNumber == licenceNumber)
                    {
                        int choice = 0;
                        do
                        {
                            Console.WriteLine("Press:\n     1: refueling.\n     2: handling.");
                            string option = Console.ReadLine();
                            Int32.TryParse(option, out choice);
                        }
                        while (choice != 1 && choice != 2);

                        switch (choice)
                        {
                            case 1:
                                bus.FuelStatus = 1200;
                                check = true;
                                Console.WriteLine("The refueling was perfomed successfully.");
                                break;
                            case 2:
                                bus.LastTreatment = DateTime.Now;//////////////////
                                bus.Maintenance = 0;
                                check = true;
                                Console.WriteLine("The treatment was perfomed successfully.");
                                Console.WriteLine("The next treatment needs to be perfomed on: {0}\\{1}\\{2}.", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year + 1);
                                break;
                            default: break;
                        }
                    }
                }
                if (!check)
                {
                    Console.WriteLine("The license number you entered was not found in the system. Please enter another number.");
                }
            }
            while (!check);
        }

        private static void chooseBus()//this func chooses a bus for driving
        {
            bool check = false;
            do
            {
                string licenceNumber = getLicenseNumber();
                foreach (Bus bus in buses)
                {
                    if (bus.LicenseNumber == licenceNumber)
                    {
                        Random km = new Random(DateTime.Now.Millisecond);
                        int kilometer = km.Next(1201);
                        if (kilometer <= bus.FuelStatus)
                        {
                            DateTime currentDate = DateTime.Now;
                            if (bus.LastTreatment.Year + 1 < currentDate.Year)
                            {
                                Console.WriteLine("It has been over a year since your last treatment.\nPut your bus in for annual treatment.");
                                check = true;
                                return;
                            }
                            else if (bus.LastTreatment.Year + 1 == currentDate.Year)
                            {
                                if (bus.LastTreatment.Month < currentDate.Month)
                                {
                                    Console.WriteLine("It has been over a year since your last treatment.\nPut your bus in for annual treatment.");
                                    check = true;
                                    return;
                                }
                                else if (bus.LastTreatment.Month == currentDate.Month)
                                {
                                    if (bus.LastTreatment.Day <= currentDate.Day)
                                    {
                                        Console.WriteLine("It has been over a year since your last treatment.\nPut your bus in for annual treatment.");
                                        check = true;
                                        return;
                                    }
                                }
                            }
                            if (bus.Maintenance > 20000)
                            {
                                Console.WriteLine("The bus passed the maximum mileage since the last treatment.\nPut your bus in for treatment.");
                                check = true;
                                return;
                            }
                            bus.FuelStatus -= kilometer;
                            bus.TotalMileage += kilometer;
                            bus.Maintenance += kilometer;
                            check = true;
                            Console.WriteLine("The bus is good to go.");
                            return;
                        }
                        Console.WriteLine("There is not enough fuel in the tank.\nYou must fill it out.");
                    }
                }
                if (!check)
                {
                    Console.WriteLine("The bus you looking for was not found in the system.\nIf you want to look for another bus press 1, otherwise press 0.");
                    int choice = int.Parse(Console.ReadLine());
                    if (choice == 0)
                    {
                        check = true;
                    }
                }
            }
            while (!check);
        }

        private static void addBus(ref bool success)//this func adds a new bus to the buses list
        {
            bool result = true;
            do
            {
                try
                {
                    string licenceNumber = getLicenseNumber();
                    Console.WriteLine("Enter the date of the commencement activity.");
                    DateTime Date;
                    success = DateTime.TryParse(Console.ReadLine(), out Date);
                    if (!success)
                    {
                        Console.WriteLine("Error. Enter a correct date.");
                        result = false;
                        return;
                    }
                    checkLicense(ref result, licenceNumber, Date);
                    Console.WriteLine("Enter the fuel status.");
                    int fuel;
                    if (!int.TryParse(Console.ReadLine(), out fuel))
                    {
                        result = false;
                        throw new Exception("Error. Enter a correct number.");
                    }
                    buses.Add(new Bus(licenceNumber, Date, fuel));
                }

                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }
            while (!result);
        }

        private static string getLicenseNumber()//this func checks whether the input is a number or not
        {
            bool check = true;
            string licenceNumber;
            do
            {
                Console.WriteLine("Enter the licence number.");
                licenceNumber = Console.ReadLine();
                var isNum = int.TryParse(licenceNumber, out _);
                if (!isNum)
                {
                    Console.WriteLine("Error. Incorrect license number.\nEnter a license number again.");
                    check = false;
                }
            }
            while (!check);
            return licenceNumber;
        }

        private static void checkLicense(ref bool result, string licenceNumber, DateTime Date)//this func check whether the license number entered is in the correct length and year
        {
            if ((Date.Year < 2018 && licenceNumber.Length == 7) || (Date.Year >= 2018 && licenceNumber.Length == 8))
            {
                return;
            }
            else
            {
                result = false;
                throw new Exception("Error. Incorrect license number.\nEnter a license number again.");
            }
        }
    }
}

/*
    Welcome to the bus system.

Select the number of your choice:
        1: Add a bus
        2: Choose a bus
        3: Performe refueling or handling of a bus
        4: Travel presentation
        0: Exit
Enter the number of your choice:
1
Enter the licence number.
1234567
Enter the date of the commencement activity.
1 3 12
Enter the fuel status.
1078

Select the number of your choice:
        1: Add a bus
        2: Choose a bus
        3: Performe refueling or handling of a bus
        4: Travel presentation
        0: Exit
Enter the number of your choice:
1
Enter the licence number.
12345678
Enter the date of the commencement activity.
3 7 19
Enter the fuel status.
780

Select the number of your choice:
        1: Add a bus
        2: Choose a bus
        3: Performe refueling or handling of a bus
        4: Travel presentation
        0: Exit
Enter the number of your choice:
1
Enter the licence number.
9876543
Enter the date of the commencement activity.
7 11 10
Enter the fuel status.
1200

Select the number of your choice:
        1: Add a bus
        2: Choose a bus
        3: Performe refueling or handling of a bus
        4: Travel presentation
        0: Exit
Enter the number of your choice:
3
Enter the licence number.
1234567
Press:
     1: refueling.
     2: handling.
2
The treatment was perfomed successfully.
The next treatment needs to be perfomed on: 27\10\2021.

Select the number of your choice:
        1: Add a bus
        2: Choose a bus
        3: Performe refueling or handling of a bus
        4: Travel presentation
        0: Exit
Enter the number of your choice:
2
Enter the licence number.
12345678
It has been over a year since your last treatment.
Put your bus in for annual treatment.

Select the number of your choice:
        1: Add a bus
        2: Choose a bus
        3: Performe refueling or handling of a bus
        4: Travel presentation
        0: Exit
Enter the number of your choice:
2
Enter the licence number.
1234567
The bus is good to go.

Select the number of your choice:
        1: Add a bus
        2: Choose a bus
        3: Performe refueling or handling of a bus
        4: Travel presentation
        0: Exit
Enter the number of your choice:
3
Enter the licence number.
12345678
Press:
     1: refueling.
     2: handling.
2
The treatment was perfomed successfully.
The next treatment needs to be perfomed on: 27\10\2021.

Select the number of your choice:
        1: Add a bus
        2: Choose a bus
        3: Performe refueling or handling of a bus
        4: Travel presentation
        0: Exit
Enter the number of your choice:
2
Enter the licence number.
12345678
The bus is good to go.

Select the number of your choice:
        1: Add a bus
        2: Choose a bus
        3: Performe refueling or handling of a bus
        4: Travel presentation
        0: Exit
Enter the number of your choice:
3
Enter the licence number.
1234567
Press:
     1: refueling.
     2: handling.
1
The refueling was perfomed successfully.

Select the number of your choice:
        1: Add a bus
        2: Choose a bus
        3: Performe refueling or handling of a bus
        4: Travel presentation
        0: Exit
Enter the number of your choice:
4
Bus number: 1.
        License number: 12-345-67.
        Commencement of activity: 01/03/2012 0:00:00.
        Total mileage: 414.
        Last maintenance day: 27/10/2020 23:21:02.
        Kilometer driven since last maintenance: 0.
        Kilometers left from last fueling: 1200.

Bus number: 2.
        License number: 123-45-678..
        Commencement of activity: 03/07/2019 0:00:00.
        Total mileage: 55.
        Last maintenance day: 27/10/2020 23:21:49.
        Kilometer driven since last maintenance: 0.
        Kilometers left from last fueling: 725.

Bus number: 3.
        License number: 98-765-43.
        Commencement of activity: 07/11/2010 0:00:00.
        Total mileage: 0.
        Last maintenance day: 01/01/0001 0:00:00.
        Kilometer driven since last maintenance: 0.
        Kilometers left from last fueling: 1200.


Select the number of your choice:
        1: Add a bus
        2: Choose a bus
        3: Performe refueling or handling of a bus
        4: Travel presentation
        0: Exit
Enter the number of your choice:
0
*/
