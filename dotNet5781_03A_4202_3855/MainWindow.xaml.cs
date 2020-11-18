﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet_02_4202_3855;
using dotNet5781_02_4202_3855;  
namespace dotNet5781_03A_4202_3855
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
        }
        private static initbusApp()
        {
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

        }
    }
   
}
