using BO;
using F_BuisnessLayer;
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

namespace E_PresentationLayer
{
    /// <summary>
    /// Interaction logic for PresentBuses.xaml
    /// </summary>
    public partial class PresentBusesWindow : Window
    {
        public ObservableCollection<BusBO> Busses { get; }
        public IBL Bl { get; }

        public PresentBusesWindow(ObservableCollection<BusBO> busses, IBL bl)
        {
            Busses = busses;
            Bl = bl;
        }


        public PresentBusesWindow()
        {
            InitializeComponent();
            lbBusDetails.ItemsSource = Busses;
        }

       
    }
}
