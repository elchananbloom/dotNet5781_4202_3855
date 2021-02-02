using BO;
using BuisnessLayer;
using System;
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
using System.Windows.Shapes;


namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for UpdateStationLineWindow.xaml
    /// </summary>
    public partial class UpdateStationLineWindow : Window
    {
        IBL bl = BLFactory.BlInstance;
        StationLineBO StationLine { get; set; }
        public UpdateStationLineWindow(StationLineBO stationLine)
        {
            StationLine = stationLine;
            InitializeComponent();
            lblStations.Content = stationLine.Station.StationName + " " + stationLine.Station.StationNumber;
            //timeTextBox.Text = stationLine.Time.ToString();
            distanceTextBox.Text = stationLine.Distance.ToString();
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
            //int a = int.Parse(stationLine.Time.Hours.ToString());
            cbHours.SelectedIndex = int.Parse(stationLine.Time.Hours.ToString());
            cbMinutes.SelectedIndex = int.Parse(stationLine.Time.Minutes.ToString());
            cbSeconds.SelectedIndex = int.Parse(stationLine.Time.Seconds.ToString());
        }

        private void btnUpdateStationLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateStationLine(new StationLineBO
                {
                    Deleted = false,
                    Distance = int.Parse(distanceTextBox.Text),
                    LineNumber = StationLine.LineNumber,
                    NumberStationInLine = StationLine.NumberStationInLine,
                    Station = bl.GetOneStation(int.Parse(String.Join("", lblStations.Content.ToString().Where(char.IsDigit)))),
                    Time = new TimeSpan(int.Parse(cbHours.SelectedItem.ToString()), int.Parse(cbMinutes.SelectedItem.ToString()), int.Parse(cbSeconds.SelectedItem.ToString()))
                });
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            this.Close();
        }
    }


}
