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
    /// Interaction logic for LineDetailsWindow.xaml
    /// </summary>
    public partial class LineDetailsWindow : Window
    {
        public BusLineBO BusLineBO { get; set; }
        public TextBox TbBusLine { get; }
        public ObservableCollection<StationLineBO> StationLines { get; set; }
        IBL bl = BLFactory.BlInstance;
        public LineDetailsWindow(BusLineBO busLineBO, TextBox tbBusLine)
        {
            BusLineBO = busLineBO;
            TbBusLine = tbBusLine;
            InitializeComponent();
            
            StationLines = new ObservableCollection<StationLineBO>(bl.GetAllStationLines(busLineBO.LineNumber));
            lblArea.Content = busLineBO.Area;
            lblLineNumber.Content = busLineBO.LineNumber;
            lbStationLines.ItemsSource = StationLines;
                                         //select this.bl.GetOneStation(item.Station.StationNumber);
            //Label tbBus = lbStationLines.DataContext as Label;
            //StationBO busBO = (StationBO)tbBus.DataContext;
            //lbStationLines.DataContext = busBO.StationName;
            //StationBO stationBO = (StationBO)lbStationLines.DataContext;
            //lbStationLines.DataContext = stationBO.StationName;
        }

        private void btnUpdateStationLine_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as FrameworkElement;
            var stationLine = s.DataContext as StationLineBO;
            UpdateStationLineWindow updateStationLine = new UpdateStationLineWindow(stationLine);
            updateStationLine.ShowDialog();
            StationLines = new ObservableCollection<StationLineBO>(bl.GetAllStationLines(stationLine.LineNumber));
            //StationLines.Remove(stationLine);
            lbStationLines.ItemsSource = StationLines;
            lbStationLines.Items.Refresh();
        }

        private void btnDeleteStationLine_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as FrameworkElement;
            var stationLine = s.DataContext as StationLineBO;
            if (MessageBox.Show("Are you sure you want to delete station line: " + stationLine.Station.StationName + '?', "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                bl.RemoveStationLine(stationLine);
                StationLines = new ObservableCollection<StationLineBO>(bl.GetAllStationLines(stationLine.LineNumber));
                //StationLines.Remove(stationLine);
                lbStationLines.ItemsSource = StationLines;
                lbStationLines.Items.Refresh();
            }
        }

        private void btnAddStationLine_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as FrameworkElement;
            var stationLine = s.DataContext as StationLineBO;
            AddStationLineWindow addStationLine = new AddStationLineWindow(stationLine);
            addStationLine.ShowDialog();
            StationLines = new ObservableCollection<StationLineBO>(bl.GetAllStationLines(stationLine.LineNumber));
            //StationLines.Remove(stationLine);
            lbStationLines.ItemsSource = StationLines;
            lbStationLines.Items.Refresh();
        }
        //public LineDetailsWindow()
        //{
        //}
    }
}
