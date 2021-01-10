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
    /// Interaction logic for UpdateBusLIneWindow.xaml
    /// </summary>
    public partial class UpdateBusLIneWindow : Window
    {
        BO.BusLineBO busLine;
        public BO.BusLineBO BusLine { get =>busLine; }
        public ObservableCollection<StationBO> Stations { get; }
        IBL bl = BLFactory.BlInstance;
        public UpdateBusLIneWindow(ObservableCollection<StationBO> stations, BusLineBO busLineBO)
        {

            Stations = stations;
            InitializeComponent();
            busLine = busLineBO;
            lblLineNumber.Content = busLineBO.LineNumber;
            cbArea.ItemsSource = Enum.GetValues(typeof(Area)).Cast<Area>().ToList();
            cbArea.SelectedIndex = (int)busLineBO.Area - 1;
            cbFirstStation.ItemsSource = from item in Stations
                                         select item.StationNumber + ", " + item.StationName;
            //cbArea = busLineBO.Area;
            StationBO stationBO = bl.GetOneStation(busLineBO.StationLines.ElementAt(0).Station.StationNumber);
            int indexAt = Stations.IndexOf(Stations.FirstOrDefault(s=>s.StationNumber==stationBO.StationNumber));
            cbFirstStation.SelectedIndex = indexAt;
            //cbFirstStation.SelectedItem = cbFirstStation.FindName(busLineBO.StationLines.ElementAt(0).Station.StationNumber + ", " + busLineBO.StationLines.ElementAt(0).Station.StationName);
            cbLastStation.ItemsSource = from item in Stations
                                        select item.StationNumber + ", " + item.StationName;
            stationBO = bl.GetOneStation(busLineBO.StationLines.ElementAt(busLineBO.StationLines.Count()-1).Station.StationNumber);
            indexAt = Stations.IndexOf(Stations.FirstOrDefault(s => s.StationNumber == stationBO.StationNumber));
            cbLastStation.SelectedIndex = indexAt;
        }
        

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

        private void btnUpdateLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BusLineBO busLineBO = new BusLineBO();
                busLineBO.Area = (Area)Enum.Parse(typeof(Area), cbArea.Text);
                busLineBO.LineNumber = (int)lblLineNumber.Content;
                if (cbFirstStation.SelectedItem == null || cbLastStation.SelectedItem == null || cbArea.SelectedItem == null)
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
                    NumberStationInLine = BusLine.StationLines.Count(),
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

                bl.UpdateBusLine(BusLine, firstStationLineBO, lastStationLineBO);
                //bl.AddBusLine(busLineBO, firstStationLineBO, lastStationLineBO);
                //PresentLinesWindow.BusLines.Add(Bl.GetOneBusLine(busLineBO.LineNumber));
                busLine = bl.GetOneBusLine(busLineBO.LineNumber);
                //BusLines.Add(Bl.GetOneBusLine(busLineBO.LineNumber));
                DialogResult = true;
                this.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        
    }
    }
}
