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
    /// Interaction logic for LineDetailsWindow.xaml
    /// </summary>
    public partial class LineDetailsWindow : Window
    {
        public BusLineBO BusLineBO { get; }
        public TextBox TbBusLine { get; }
        IBL bl = BLFactory.BlInstance;
        public LineDetailsWindow(BusLineBO busLineBO, TextBox tbBusLine)
        {
            BusLineBO = busLineBO;
            TbBusLine = tbBusLine;
            InitializeComponent();
            lblLineNumber.Content = busLineBO.LineNumber;
            lblArea.Content = busLineBO.Area;
            lbStationLines.ItemsSource = from item in busLineBO.StationLines
                                         select item;
                                         //select this.bl.GetOneStation(item.Station.StationNumber);
            //Label tbBus = lbStationLines.DataContext as Label;
            //StationBO busBO = (StationBO)tbBus.DataContext;
            //lbStationLines.DataContext = busBO.StationName;
            //StationBO stationBO = (StationBO)lbStationLines.DataContext;
            //lbStationLines.DataContext = stationBO.StationName;
        }
        //public LineDetailsWindow()
        //{
        //}
    }
}
