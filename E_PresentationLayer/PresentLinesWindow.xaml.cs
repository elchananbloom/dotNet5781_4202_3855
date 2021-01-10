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
    /// Interaction logic for PresentLinesWindow.xaml
    /// </summary>
    public partial class PresentLinesWindow : Window
    {
        public ObservableCollection<BusLineBO> BusLines { get; set; }
        public ObservableCollection<StationBO> Stations { get; }
        IBL bl = BLFactory.BlInstance;

        public PresentLinesWindow()
        {
            BusLines = new ObservableCollection<BO.BusLineBO>(bl.GetAllBusLines());
            Stations = new ObservableCollection<BO.StationBO>(bl.GetAllStations());

            InitializeComponent();
            lbLines.DataContext = BusLines;
        }

        private void tbLines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox tbBusLine = sender as TextBox;
            BusLineBO busLineBO = (BusLineBO)tbBusLine.DataContext;
            busLineBO.StationLines = new ObservableCollection<StationLineBO>(bl.GetAllStationLines(busLineBO.LineNumber));
            LineDetailsWindow lineDetailsWindow = new LineDetailsWindow(busLineBO, tbBusLine);
            lineDetailsWindow.ShowDialog();
        }

        private void btnAddLine_Click(object sender, RoutedEventArgs e)
        {
            //AddLineWindow wnd = new AddLineWindow();
            //bool? result = wnd.ShowDialog();
            //if(result == true)
            //{
            //    BusLines.Add( wnd.BusLine);
            //}


            AddBusLineWindow addBusLineWindow = new AddBusLineWindow(Stations);
            bool? result = addBusLineWindow.ShowDialog();
            if (result==true)
            {
                BusLines = new ObservableCollection<BO.BusLineBO>(bl.GetAllBusLines());
                lbLines.ItemsSource = BusLines;
                lbLines.Items.Refresh();
                //BusLines.Add(addBusLineWindow.BusLine);
            }
            //BusLines = from item in Bl.GetAllBusLines()
            // select item;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentBus = sender as FrameworkElement;
            var busLineBO = currentBus.DataContext as BusLineBO;
            if (MessageBox.Show("Are you sure you want to delete line: " + busLineBO.LineNumber + '?', "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                bl.RemoveBusLine(busLineBO);
                BusLines.Remove(busLineBO);
                //BusLines = new ObservableCollection<BO.BusLineBO>(Bl.GetAllBusLines());

                //lbLines.ItemsSource = BusLines;

            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var currentBus = sender as FrameworkElement;
            var busLineBO = currentBus.DataContext as BusLineBO;
            UpdateBusLIneWindow updateBusLIneWindow = new UpdateBusLIneWindow(Stations,busLineBO);
            bool? result = updateBusLIneWindow.ShowDialog();
            if (result == true)
            {
                BusLines = new ObservableCollection<BO.BusLineBO>(bl.GetAllBusLines());
                lbLines.ItemsSource = BusLines;
                lbLines.Items.Refresh();
                //BusLines.Add(addBusLineWindow.BusLine);
            }
        }

        //public PresentLinesWindow()
        //{
        //}




    }
}
