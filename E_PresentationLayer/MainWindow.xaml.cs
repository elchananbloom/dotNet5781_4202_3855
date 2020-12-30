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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E_PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ObservableCollection<BO.BusBO> busses = new ObservableCollection<BO.BusBO>();
        IBL bl = BLFactory.BlInstance;

        public MainWindow()
        {
            InitializeComponent();
            busses = new ObservableCollection<BO.BusBO>(bl.GetAllBusesBO());
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            ManagementWindow managementWindow = new ManagementWindow(busses, bl);
            managementWindow.ShowDialog();


        }
    }
}
