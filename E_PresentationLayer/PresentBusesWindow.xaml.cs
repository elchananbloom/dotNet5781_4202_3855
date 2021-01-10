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
    /// Interaction logic for PresentBuses.xaml
    /// </summary>
    public partial class PresentBusesWindow : Window
    {
        public ObservableCollection<BusBO> Busses { get; }
        IBL bl = BLFactory.BlInstance;

        public PresentBusesWindow()
        {
            Busses = new ObservableCollection<BO.BusBO>(bl.GetAllBusesBO());

            InitializeComponent();
            lbBusDetails.ItemsSource = Busses;
        }


        

        private void tbMyBus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox tbBus = sender as TextBox;
            BusBO busBO = (BusBO)tbBus.DataContext;
            BusDetailsWindow details = new BusDetailsWindow(busBO, tbBus);
            details.ShowDialog();
        }
    }
}
