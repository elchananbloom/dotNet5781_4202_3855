using BO;
using BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for AddBusLineWindow.xaml
    /// </summary>
    public partial class AddBusLineWindow : Window
    {
        BO.BusLineBO busLine;
        //public ObservableCollection<BusLineBO> BusLines { get; }
        public BO.BusLineBO BusLine { get => busLine; }
        public ObservableCollection<StationBO> Stations { get; }
        IBL bl = BLFactory.BlInstance;

        public AddBusLineWindow(ObservableCollection<StationBO> stations)
        {
            //BusLines = busLines;
            Stations = stations;
            InitializeComponent();
            busLine = new BusLineBO();
            cbArea.ItemsSource = Enum.GetValues(typeof(Area)).Cast<Area>().ToList();
            cbFirstStation.ItemsSource = from item in Stations
                                         select item.StationNumber + ", " + item.StationName;
            cbLastStation.ItemsSource = from item in Stations
                                        select item.StationNumber + ", " + item.StationName;
        }
        //public AddBusLineWindow()
        //{
        //}

        private void cbFirstStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFirstStation.SelectedItem != null && cbLastStation.SelectedItem != null)
            {
                if (cbFirstStation.SelectedItem.ToString() == cbLastStation.SelectedItem.ToString())
                {
                    cbFirstStation.SelectedItem = null;
                    MessageBox.Show("You have choosed the same station for the first and the last station.\nPlease change one of them.");
                }
            }
        }

        private void cbLastStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFirstStation.SelectedItem != null && cbLastStation.SelectedItem != null)
            {
                if (cbFirstStation.SelectedItem.ToString() == cbLastStation.SelectedItem.ToString())
                {
                    cbLastStation.SelectedItem = null;
                    MessageBox.Show("You have choosed the same station for the first and the last station.\nPlease change one of them.");
                }
            }
        }

        private void btnAddLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BusLineBO busLineBO = new BusLineBO();
                busLineBO.Area = (Area)Enum.Parse(typeof(Area), cbArea.Text);
                busLineBO.LineNumber = int.Parse(tbLineNumber.Text);
                if(cbFirstStation.SelectedItem==null||cbLastStation.SelectedItem==null||cbArea.SelectedItem==null||tbLineNumber.Text==null)
                {
                    throw new ArgumentNullException("Cannot add this bus line."); 
                }

                StationBO stationBO = bl.GetOneStation(int.Parse(String.Join("", cbFirstStation.SelectedItem.ToString().Where(char.IsDigit))));
                StationLineBO firstStationLineBO = new StationLineBO
                {
                    Deleted = false,
                    LineNumber = busLineBO.LineNumber,
                    NumberStationInLine = 0,
                    Station = stationBO
                };
                stationBO = bl.GetOneStation(int.Parse(String.Join("", cbLastStation.SelectedItem.ToString().Where(char.IsDigit))));
                StationLineBO lastStationLineBO = new StationLineBO
                {
                    Deleted = false,
                    LineNumber = busLineBO.LineNumber,
                    NumberStationInLine = 1,
                    Station = stationBO
                };
                //busLineBO.StationLines = new ObservableCollection<StationLineBO>();
                //busLineBO.StationLines.ToList().Add(lastStationLineBO);
                //busLineBO.StationLines.ToList().Add(firstStationLineBO);

                //Bl.AddStationLine(firstStationLineBO);
                //Bl.AddStationLine(lastStationLineBO);
                //busLineBO.StationLines = from item in Bl.GetAllStationLines(busLineBO.LineNumber)
                //                         select item;
                //busLineBO.StationLines.ToList()[0] = cbFirstStation.SelectedItem as StationLineBO;
                //busLineBO.StationLines.ToList()[1] = cbLastStation.SelectedItem as StationLineBO;

                bl.AddBusLine(busLineBO, firstStationLineBO, lastStationLineBO);
                //PresentLinesWindow.BusLines.Add(Bl.GetOneBusLine(busLineBO.LineNumber));
                busLine = bl.GetOneBusLine(busLineBO.LineNumber);
                //BusLines.Add(Bl.GetOneBusLine(busLineBO.LineNumber));
                DialogResult = true;
                this.Close();
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message + error.InnerException);
            }
        }

        /// <summary>
        /// the 3 following functions are making sure that the input from the user are numbers. 
        /// </summary>
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void tbLineNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
