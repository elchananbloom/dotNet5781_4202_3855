using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for TravelWindow.xaml
    /// </summary>
    public partial class TravelWindow : Window
    {
        private Bus currentBus;
        public Bus CurrentBus { get => currentBus; }
        public TravelWindow(Bus bus)
        {
            currentBus = bus;
            InitializeComponent();
        }

        private void tbKms_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Return)
            {
                try
                {
                    string km = this.tbKms.Text;
                    int num = int.Parse(km);
                    if (currentBus.FuelStatus - num < 0 )
                    {
                        throw new ArgumentOutOfRangeException("There is not enough fuel in the tank.");
                    }
                    if (currentBus.Maintenance + num > 20000 || currentBus.LastTreatment.AddYears(1) < DateTime.Now)
                    {
                        throw new ArgumentOutOfRangeException("The bus needs to be sent for treatment.");
                    }
                    //if (currentBus.Status != (int)BusStatus.READY_TO_DRIVE)
                    //{
                    //    throw new InvalidOperationException("The bus isn't ready to drive.");
                    //}
                    currentBus.FuelStatus -= num;
                    currentBus.Maintenance += num;
                    currentBus.TotalMileage += num;
                    Thread.Sleep(500);
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
                finally
                {
                    this.Close();
                }
            }
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void tbKms_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);   
        }
    }
}
