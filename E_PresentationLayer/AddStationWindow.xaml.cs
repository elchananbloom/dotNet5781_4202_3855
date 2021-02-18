using BO;
using BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        StationBO station;
        IBL bl = BLFactory.BlInstance;
        public StationBO Station { get; set; }
        public AddStationWindow()
        {
            InitializeComponent();


        }

        private void btnAddStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbLatitude.Text == null || tbLongtitude.Text == null || tbStationName.Text == null || tbStationNumber.Text == null)
                {
                    MessageBox.Show("Error. At least one field has not been filled.");
                }
                else if (float.Parse(tbLatitude.Text) < 31 || float.Parse(tbLatitude.Text) > 33.3)
                {
                    MessageBox.Show("Error. The latitude is out of the range 31-33.3");
                }
                else if (float.Parse(tbLongtitude.Text) < 34.3 || float.Parse(tbLongtitude.Text) > 35.5)
                {
                    MessageBox.Show("Error. The longitude is out of the range 34.3-35.5");
                }
                else
                {
                    Station = new StationBO
                    {
                        Deleted = false,
                        Latitude = float.Parse(tbLatitude.Text),
                        Longtitude = float.Parse(tbLongtitude.Text),
                        StationName = tbStationName.Text,
                        StationNumber = int.Parse(tbStationNumber.Text),
                        BusLinesInStation = bl.GetAllBusLinesInStation(int.Parse(tbStationNumber.Text))
                    };
                    bl.AddStation(Station);
                    DialogResult = true;
                    this.Close();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message + error.InnerException);
            }
        }
        /// <summary>
        /// the 3 following functions are making sure that the input from the user are numbers. 
        /// </summary>
        private static readonly Regex _regex = new Regex("[^0-9.]+");

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void tbLatitude_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void tbLongtitude_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
