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
        public ObservableCollection<StationBO> Stations { get; }
        IBL bl = BLFactory.BlInstance;

        public PresentStationsWindow()
        {
            Stations = new ObservableCollection<BO.StationBO>(bl.GetAllStations());
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




    }
}
