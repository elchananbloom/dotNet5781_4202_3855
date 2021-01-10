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
using BuisnessLayer;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for AddLineWindow.xaml
    /// </summary>
    public partial class AddLineWindow : Window
    {
        BO.BusLineBO busLineBO;
        IBL bl = BLFactory.BlInstance;

        public BO.BusLineBO BusLine { get => busLineBO; }
        public AddLineWindow()
        {
            InitializeComponent();
            busLineBO = new BO.BusLineBO();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bl.AddBusLine(BusLine, null, null);

            DialogResult = true;
            this.Close();
        }
    }
}
