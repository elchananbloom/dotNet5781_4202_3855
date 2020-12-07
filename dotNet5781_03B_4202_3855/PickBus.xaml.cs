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
    /// Interaction logic for PickBus.xaml
    /// </summary>
    public partial class PickBus : Window
    {
        private Bus el;
        public Bus al { get => el; set => el = value; }
        public PickBus(Bus bus)
        {
            InitializeComponent();
            al = bus;
        }

        private void tbDistance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    string km = tbDistance.Text;
                    int num = int.Parse(km);
                    if (el.FuelStatus < num)
                    {
                        throw new ArgumentOutOfRangeException("There is not enough fuel in the tank.");
                    }
                    if (el.Maintenance + num > 20000 || el.LastTreatment.AddYears(1) < DateTime.Now)
                    {
                        throw new ArgumentOutOfRangeException("The bus needs to be sent for treatment.");
                    }
                    if (el.Status != (int)BusStatus.READY_TO_DRIVE)
                    {
                        throw new InvalidOperationException("The bus isn't ready to drive.");
                    }
                    el.FuelStatus -= num;
                    this.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    this.Close();
                }
            }
            this.Close();
        }

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    this.DialogResult = true;
        //    this.Close();
        //}


    }
}
