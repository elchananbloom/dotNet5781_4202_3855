using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

// שומר את כל הנתונים של כל הישויות של הDALAPI ומאתחל אותם.
namespace DataSource
{
    public class DataSource
    {
        /// <summary>
        /// Bus repository.
        /// </summary>
        private static List<BusDAO> bussesList = new List<BusDAO>();
        private static List<BusInTravelDAO> busestravelList = new List<BusInTravelDAO>();
        private static List<BusLineDAO> busLinesList = new List<BusLineDAO>();
        private static List<StationDAO> stationsList = new List<StationDAO>();
        private static List<StationLineDAO> stationLinesList = new List<StationLineDAO>();
        private static List<CoupleStationInRowDAO> coupleStationInRowList = new List<CoupleStationInRowDAO>();
        private static List<LineInServiceDAO> lineInServicesList = new List<LineInServiceDAO>();
        /// <summary>
        /// properties.
        /// </summary>
        public static List<BusDAO> BussesList { get => bussesList; }
        public static List<BusInTravelDAO> BussesInTravelList { get => busestravelList; }
        public static List<BusLineDAO> BusLinesList { get => busLinesList; }
        public static List<StationDAO> StationsList { get => stationsList; }
        public static List<StationLineDAO> StationLinesList { get => stationLinesList; }
        public static List<CoupleStationInRowDAO> CoupleStationInRowList { get => coupleStationInRowList; }
        public static List<LineInServiceDAO> LineInServicesList { get => lineInServicesList; }


        /// <summary>
        /// Initializing the BusDAO Yeshot.
        /// </summary>
        //public static void initBuses()
        //{
        //    BussesList.Add(new BusDAO
        //    {
        //        LicenseNumber = "1234567",
        //        StartOfWork = DateTime.Today.AddYears(-3),
        //        TotalKms = 5000,
        //        Fuel = 1200,
        //        Status = Status.READY_TO_DRIVE
        //    });
        //    BussesList.Add(new BusDAO
        //    {
        //        LicenseNumber = "3333333",
        //        StartOfWork = DateTime.Today.AddYears(-20),
        //        TotalKms = 9999999,
        //        Fuel = 500,
        //        Status = Status.READY_TO_DRIVE
        //    });
        //    BussesList.Add(new BusDAO
        //    {
        //        LicenseNumber = "77745617",
        //        StartOfWork = DateTime.Today.AddYears(-2),
        //        TotalKms = 5000,
        //        Fuel = 1200,
        //        Status = Status.READY_TO_DRIVE
        //    });
        //    BussesList.Add(new BusDAO
        //    {
        //        LicenseNumber = "6666666",
        //        StartOfWork = DateTime.Today.AddYears(-100),
        //        TotalKms = 5000,
        //        Fuel = 1200,
        //        Status = Status.READY_TO_DRIVE
        //    });
        //}
        
        /// <summary>
        /// this func initialize the 50 stations, 10 lines with at least 10 stations, 20 buses etc...
        /// </summary>
        public static void init()
        {
            //int num = 0;
            string userChoice = string.Empty;
            //BusLinesCollection busLines = new BusLinesCollection();
            int[] stationKeys = new int[50];
            string[] addres = { "Malissa", "Barton", "Heide", "Quintin", "Lula", "Ione", "Larhonda", "Yuko", "Wendolyn", "Willene", "Mitsue", "Ellsworth", "Angla", "Scarlet", "Xiao", "Khalilah", "Charlsie", "Norbert", "Ria", "Ladonna", "Jama", "Earlene", "Sylvia", "Ellie", "Loyce", "Meagan", "Oretha", "Corina", "Joelle", "Arthur", "Larita", "Dillon", "Beulah", "Fritz", "Danyelle", "Kerstin", "Deirdre", "Kathi", "Dodie", "Angelika", "Liam", "Olivia", "Noah", "Emma", "Oliver", "Ava", "William", "Sophia",
                "Elijah","Isabella" }; 
            Random rand = new Random();
            double latitude;
            double longitude;
            int busStationDist;
            TimeSpan travelTime;

            //Initialize 50 stations.
            for (int i = 0; i < 50; i++)
            {
                stationKeys[i] = i + 25487 - 15 * i;
                latitude = NextDouble(rand, 31.0, 33.3);
                longitude = NextDouble(rand, 34.3, 35.5);
                busStationDist = rand.Next(500);
                travelTime = new TimeSpan(rand.Next(2), rand.Next(59), rand.Next(59));
                //StationDAO busSt = new StationDAO(busStationDist, travelTime, stationKeys[i], addres[i], latitude, longitude);
                StationsList.Add(new StationDAO
                {
                    StationNumber= stationKeys[i],
                    Longtitude=longitude,
                    Latitude=latitude,
                    StationName= addres[i],
                    Deleted=false
                });
            }

            //the line num
            Random randLineNum = new Random();
            int[] area = { 2, 3, 4, 1, 5, 1, 3, 2, 4, 5 };
            int index = 0;
            //int anotherIndex = 0;
            int[] lineNumber = { randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550) };
            //lineNumber[anotherIndex], stations[index++], stations[index++], area[anotherIndex++

            //Initialize 10 buses (implementation is at the buttom).
            //initBus();
            string[] licenseNum = { "5632357", "57643276", "9853435", "56623267", "8534356", "5452537", "57342125", "56745257", "9743247", "2378635" };
            int[] mainten = new int[10];
            //Random rand = new Random();
            for (int i = 0; i < 9; i++)
            {
                mainten[i] = rand.Next(20000);
            }
            mainten[9] = 19950;
            DateTime[] lastTreat = { new DateTime(2020, 9, 23), new DateTime(2020, 7, 13), new DateTime(2020, 11, 9), new DateTime(2020, 10, 14), new DateTime(2020, 1, 8), new DateTime(2019, 12, 22),
                new DateTime(2018, 6, 15),new DateTime(2020, 4, 27),new DateTime(2020, 8, 29),new DateTime(2020, 7, 4) };
            DateTime[] dateBegin = { new DateTime(1996, 5, 7), new DateTime(2019, 12, 17), new DateTime(2015, 3, 26), new DateTime(2020, 5, 7), new DateTime(2010, 2, 16), new DateTime(2007, 6, 11),
                new DateTime(2018, 3, 29),new DateTime(2019, 8, 23),new DateTime(2002, 4, 15),new DateTime(1999, 5, 17) };
            int[] fuel = new int[10];
            for (int i = 1; i < 10; i++)
            {
                fuel[i] = rand.Next(1000);
            }
            fuel[0] = 50;
            for (int i = 0; i < 10; i++)
            {
                BussesList.Add(new BusDAO
                {
                    LicenseNumber = licenseNum[i],
                    Maintnance = mainten[i],
                    LastTreatment = lastTreat[i],
                    TotalKms = rand.Next(500000),
                    Fuel = fuel[i],
                    StartOfWork = dateBegin[i],
                    Deleted = false
                });
            }
            //Initialize 10 lines.
            for (int i = 0; i < 10; i++)
            {
                BusLinesList.Add(new BusLineDAO
                {
                    LineNumber = lineNumber[i],
                    Area = (Area)area[i],
                    FirstStationNumber = StationsList[index++].StationNumber,
                    LastStationNumber = StationsList[index++].StationNumber,
                    Deleted = false,
                    CurrentSerialNB = Configuration.SerialBusLine
                }); 
            }

            //initialize 10 station lines to 10 bus lines.
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    StationLineDAO newStationLine = new StationLineDAO
                    {
                        LineNumber = lineNumber[i],
                        StationNumber = StationsList[rand.Next(49)].StationNumber,
                        NumberStationInLine = j,
                        Deleted = false
                    };
                    //if (!(StationLinesList.Exists(currentStationLine => (currentStationLine.LineNumber == newStationLine.LineNumber)
                    // && (currentStationLine.StationNumber != newStationLine.StationNumber))))
                    //{
                    bool exist = false;
                    foreach (var item in StationLinesList)
                    {
                        if(item.LineNumber==newStationLine.LineNumber&&item.StationNumber==newStationLine.StationNumber)
                        {
                            exist = true;
                        }
                    }
                    if (exist)
                    {
                        j--;
                    }
                    else
                    {
                        StationLinesList.Add(newStationLine);
                    }
                    //}
                    //else
                    //{
                    //    j--;
                    //}
                }
            }

            for (int i = 0; i < 10; i++)
            {
                LineInServicesList.Add(new LineInServiceDAO
                {
                    LineInServiceSerialNB = Configuration.SerialLineInService,
                    LineNumber = lineNumber[i],
                    StartLineTime = new TimeSpan(rand.Next(23), 0, 0),
                    Frequency = new TimeSpan(rand.Next(1), rand.Next(59), 0),
                    EndLineTime = new TimeSpan(rand.Next(23), 0, 0)
                });
            }
            //index = 10;
            //int stationsIndex = 0;
            //int lineNumIndex = 0;

            //for (int i = 0; i < 9; i++)
            //{
            //    busLines[lineNumber[lineNumIndex++], stations[stationsIndex]].AddStationToLineRoute(stations[index++], 1);
            //    stationsIndex += 2;
            //}
            //busLines[lineNumber[9], stations[stationsIndex]].AddStationToLineRoute(stations[9], 1);
            //lineNumIndex = 0;
            //stationsIndex = 0;
            //for (int i = 0; i < 20; i++)
            //{
            //    busLines[lineNumber[lineNumIndex++], stations[stationsIndex]].AddStationToLineRoute(stations[index++], 1);
            //    stationsIndex += 2;
            //    if (lineNumIndex == 10)
            //    {
            //        lineNumIndex = 0;
            //        stationsIndex = 0;
            //    }
            //}
        }

        /// <summary>
        /// initializing 10 busses
        /// </summary>
        //private static void initBus()
        //{
            
        //}

        //this func is made for generating the random number of the station's location, (since it is a double type range).
        static double NextDouble(Random rand, double minValue, double maxValue)
        {
            return rand.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
