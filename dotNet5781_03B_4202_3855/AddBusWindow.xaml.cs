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
    public partial class AddBusWindow : Window
    {
        private Bus bus;
        public Bus AddedBus { get => bus; }

        /// <summary>
        /// ctor.
        /// </summary>
        public AddBusWindow()
        {
            bus = new Bus();
            InitializeComponent();
            this.DataContext = bus;
        }

        /// <summary>
        /// this func starts the func of the addbus in the main-window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
