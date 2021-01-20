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
    /// Interaction logic for PresentStationsWindow.xaml
    /// </summary>
    public partial class PresentStationsWindow : Window
    {
        public ObservableCollection<StationBO> Stations { get; set; }
        IBL bl = BLFactory.BlInstance;

        public PresentStationsWindow()
        {
            Stations = new ObservableCollection<StationBO>(bl.GetAllStations());
            InitializeComponent();
            lbStation.ItemsSource = Stations;
        }

        private void tbStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox tbStation = sender as TextBox;
            StationBO stationBO = (StationBO)tbStation.DataContext;
            StationDetailsWindow stationDetailsWindow = new StationDetailsWindow(stationBO,bl);
            stationDetailsWindow.ShowDialog();
        }
        //public PresentStationsWindow()
        //{
        //}

        private void btnAddStation_Click(object sender, RoutedEventArgs e)
        {
            AddStationWindow addStationWindow = new AddStationWindow();
            bool? result = addStationWindow.ShowDialog();
            if(result==true)
            {
                Stations = new ObservableCollection<StationBO>(bl.GetAllStations());
                lbStation.ItemsSource = Stations;
                lbStation.Items.Refresh();
            }
        }

        private void btnDeleteStation_Click(object sender, RoutedEventArgs e)
        {
            var currentStation = sender as FrameworkElement;
            var station = currentStation.DataContext as StationBO;
            if (MessageBox.Show("Are you sure you want to delete station: " + station.StationNumber + '?', "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                bl.RemoveStation(station);
                Stations.Remove(station);
            }
        }

        private void btnUpdateStation_Click(object sender, RoutedEventArgs e)
        {
            var currentStation = sender as FrameworkElement;
            var station = currentStation.DataContext as StationBO;
            UpdateStationWindow updateStationWindow = new UpdateStationWindow(station);
            bool? result = updateStationWindow.ShowDialog();
            if (result == true)
            {
                Stations = new ObservableCollection<StationBO>(bl.GetAllStations());
                lbStation.ItemsSource = Stations;
                lbStation.Items.Refresh();
            }

        }

        //private void btnUpdate_Click(object sender, RoutedEventArgs e)
        //{
        //    var currentBus = sender as FrameworkElement;
        //    var busLineBO = currentBus.DataContext as BusLineBO;
        //    UpdateBusLIneWindow updateBusLIneWindow = new UpdateBusLIneWindow(Stations, busLineBO);
        //    bool? result = updateBusLIneWindow.ShowDialog();
        //    if (result == true)
        //    {
        //        BusLines = new ObservableCollection<BO.BusLineBO>(bl.GetAllBusLines());
        //        lbLines.ItemsSource = BusLines;
        //        lbLines.Items.Refresh();
        //        //BusLines.Add(addBusLineWindow.BusLine);
        //    }
        //}




        //public PresentLinesWindow()
        //{
        //}




    }

}

