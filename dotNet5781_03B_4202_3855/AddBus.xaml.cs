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

namespace dotNet5781_03B_4202_3855
{
    /// <summary>
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        private Bus bus;
        public Bus AddedBus { get => bus; }
        public AddBus()
        {
            bus = new Bus();
            InitializeComponent();
            this.DataContext = bus;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // busViewSource.Source = [generic data source]
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();

        }
    }
}
