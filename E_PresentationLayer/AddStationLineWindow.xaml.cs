using BO;
using BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for AddStationLineWindow.xaml
    /// </summary>
    public partial class AddStationLineWindow : Window
    {
        IBL bl = BLFactory.BlInstance;
        public StationLineBO StationLine { get; set; }
        public ObservableCollection<StationBO> Stations { get; }
        //public BusLineBO BusLine { get; set; }
        public AddStationLineWindow(StationLineBO stationLine)
        {
            StationLine = stationLine;
            //BusLine = bl.GetOneBusLine(StationLine.LineNumber);
            Stations = new ObservableCollection<StationBO>(bl.GetAllStations());
            InitializeComponent();
            cbStations.ItemsSource = from item in Stations
                                     select item.StationNumber + ", " + item.StationName;
            for (int i = 0; i <= 24; i++)
            {
                cbHours.Items.Add(i);
            }
            for (int i = 0; i <= 59; i++)
            {
                cbMinutes.Items.Add(i);
            }
            for (int i = 0; i <= 59; i++)
            {
                cbSeconds.Items.Add(i);
            }
            cbHours.SelectedIndex = 0;
            cbMinutes.SelectedIndex = 0;
            cbSeconds.SelectedIndex = 0;

        }

        private void btnAddStationLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddStationLine(new StationLineBO
                {
                    Deleted = false,
                    Distance = int.Parse(distanceTextBox.Text),
                    LineNumber = StationLine.LineNumber,
                    NumberStationInLine = StationLine.NumberStationInLine + 1,
                    Station = bl.GetOneStation(int.Parse(String.Join("", cbStations.SelectedItem.ToString().Where(char.IsDigit)))),
                    Time = new TimeSpan(int.Parse(cbHours.SelectedItem.ToString()), int.Parse(cbMinutes.SelectedItem.ToString()), int.Parse(cbSeconds.SelectedItem.ToString()))
                });
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            this.Close();
        }

        private void cbStations_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
