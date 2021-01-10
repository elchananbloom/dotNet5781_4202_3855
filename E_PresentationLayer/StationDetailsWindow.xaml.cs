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
    /// Interaction logic for StationDetailsWindow.xaml
    /// </summary>
    public partial class StationDetailsWindow : Window
    {
        public StationBO StationBO { get; }
        public IBL Bl { get; }

        public StationDetailsWindow(StationBO stationBO, IBL bl)
        {
            StationBO = stationBO;
            Bl = bl;
            InitializeComponent();
            lblStationNumber.Content = StationBO.StationNumber;
            lblStationName.Content = StationBO.StationName;
            lbBusLinesInStation.ItemsSource = Bl.GetAllBusLinesInStation(StationBO.StationNumber);
        }

        //public StationDetailsWindow()
        //{
        //}

       

        
    }
}
