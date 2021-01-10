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
    /// Interaction logic for ManagementWindow.xaml
    /// </summary>
    public partial class ManagementWindow : Window
    {
        IBL bl = BLFactory.BlInstance;


        public ManagementWindow()
        {
            
            InitializeComponent();

        }


        //public ManagementWindow()
        //{
        //}

        

        private void PresentBuses_Click_1(object sender, RoutedEventArgs e)
        {
            PresentBusesWindow presentBusesWindow = new PresentBusesWindow();
            presentBusesWindow.ShowDialog();
        }

        private void PresentLines_Click(object sender, RoutedEventArgs e)
        {
            PresentLinesWindow presentLinesWindow = new PresentLinesWindow();
            presentLinesWindow.ShowDialog();
        }

        private void PresentStations_Click(object sender, RoutedEventArgs e)
        {
            PresentStationsWindow presentStationsWindow = new PresentStationsWindow();
            presentStationsWindow.ShowDialog();
        }
    }
}
