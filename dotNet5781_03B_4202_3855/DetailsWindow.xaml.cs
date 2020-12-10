using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace dotNet5781_03B_4202_3855
{
    /// <summary>
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        private Bus el;
        public Bus al { get => el; }
        public DetailsWindow(Bus bus)
        {
            el = bus;
            InitializeComponent();
            dateBeginTextBox.Content = el.DateBegin.ToString();
            fuelStatusTextBox.Content = el.FuelStatus.ToString();
            lastTreatmentTextBox.Content = el.LastTreatment.ToString();
            licenseNumberTextBox.Content = el.LicenseNumber;
            maintenanceTextBox.Content = el.Maintenance.ToString();
            totalMileageTextBox.Content = el.TotalMileage.ToString();
            //ListBoxItem newItem = new ListBoxItem();
            //newItem.Content = el.ToString();
            //lbDetails.Items.Add(newItem);
            //DataContext = el;
            //lbDetails.DataContext = el.ToString();
        }

        private void Treatment_Click(object sender, RoutedEventArgs e)
        {
            el.LastTreatment = DateTime.Now;
            el.Maintenance = 0;
            el.Status = 1;
        }

        private void bRefueling_Click(object sender, RoutedEventArgs e)
        {
            el.FuelStatus = 1200;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }
    }
}
