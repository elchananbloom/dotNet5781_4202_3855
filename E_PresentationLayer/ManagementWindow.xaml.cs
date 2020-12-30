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
    /// Interaction logic for ManagementWindow.xaml
    /// </summary>
    public partial class ManagementWindow : Window
    {
        public ManagementWindow()
        {
            InitializeComponent();
        }

        public ManagementWindow(ObservableCollection<BusBO> busses, IBL bl)
        {
            Bl = bl;
            Busses = busses;
        }

        public IBL Bl { get; }
        public ObservableCollection<BusBO> Busses { get; }

        private void PresentBuses_Click_1(object sender, RoutedEventArgs e)
        {
            PresentBusesWindow presentBusesWindow = new PresentBusesWindow(Busses, Bl);
            presentBusesWindow.ShowDialog();
        }


    }
}
