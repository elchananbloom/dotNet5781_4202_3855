using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

// שומר את כל הנתונים של כל הישויות של הDALAPI ומאתחל אותם.
namespace I_DataSource
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
        
        /// <summary>
        /// properties.
        /// </summary>
        public static List<BusDAO> BussesList { get => bussesList; }
        public static List<BusInTravelDAO> BussesInTravelList { get => busestravelList; }
        public static List<BusLineDAO> BusLinesList { get => busLinesList; }
        public static List<StationDAO> StationsList { get => stationsList; }
        public static List<StationLineDAO> StationLinesList { get => stationLinesList; }
        public static List<CoupleStationInRowDAO> CoupleStationInRowList { get => coupleStationInRowList; }


        /// <summary>
        /// Initializing the BusDAO Yeshot.
        /// </summary>
        public static void initBuses()
        {
            BussesList.Add(new BusDAO
            {
                LicenseNumber = "1234567",
                StartOfWork = DateTime.Today.AddYears(-3),
                TotalKms = 5000,
                Fuel = 1200,
                Status = Status.READY_TO_DRIVE
            });
            BussesList.Add(new BusDAO
            {
                LicenseNumber = "3333333",
                StartOfWork = DateTime.Today.AddYears(-20),
                TotalKms = 9999999,
                Fuel = 500,
                Status = Status.READY_TO_DRIVE
            });
            BussesList.Add(new BusDAO
            {
                LicenseNumber = "77745617",
                StartOfWork = DateTime.Today.AddYears(-2),
                TotalKms = 5000,
                Fuel = 1200,
                Status = Status.READY_TO_DRIVE
            });
            BussesList.Add(new BusDAO
            {
                LicenseNumber = "6666666",
                StartOfWork = DateTime.Today.AddYears(-100),
                TotalKms = 5000,
                Fuel = 1200,
                Status = Status.READY_TO_DRIVE
            });
        }
        
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
                stationsList.Add(new StationDAO
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
            int anotherIndex = 0;
            int[] lineNumber = { randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550), randLineNum.Next(550) };
            //lineNumber[anotherIndex], stations[index++], stations[index++], area[anotherIndex++
            
            //Initialize 10 lines.
            for (int i = 0; i < 10; i++)
            {
                BusLinesList.Add(new BusLineDAO
                {
                    LineNumber = lineNumber[anotherIndex],
                    Area = (Area)area[anotherIndex++],
                    FirstStationNumber = stationsList[index++].StationNumber,
                    LastStationNumber = stationsList[index++].StationNumber,
                    Deleted = true
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
                        StationNumber = stationsList[rand.Next(49)].StationNumber,
                        NumberStationInLine = j,
                        Deleted = false
                    };
                    if (stationLinesList.Exists(currentStationLine => currentStationLine.LineNumber == newStationLine.LineNumber
                     && currentStationLine.StationNumber != newStationLine.StationNumber))
                    {
                        stationLinesList.Add(newStationLine);
                    }
                    j--;
                }
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











        //this func is made for generating the random number of the station's location, (since it is a double type range).
        static double NextDouble(Random rand, double minValue, double maxValue)
        {
            return rand.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
