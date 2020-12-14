using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private Button btnBus;
        public Bus CurrentBus { get => currentBus; }
        public Button BtnBus { get => btnBus; set => btnBus = value; }
        BackgroundWorker bgWorker = new BackgroundWorker();
       
        public TravelWindow(Bus bus,Button bBus)
        {
            BtnBus = bBus;
            currentBus = bus;
            InitializeComponent();
            
        }

        private void tbKms_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Return)
            {
                try
                {
                    ThreadProgressBar threadProgressBar = new ThreadProgressBar();
                    TextBox tbRow = new TextBox();
                    bgWorker.DoWork += BgWorker_DoWork;
                    bgWorker.ProgressChanged += BgWorker_ProgressChanged;
                    bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
                    bgWorker.WorkerSupportsCancellation = true;
                    bgWorker.WorkerReportsProgress = true;
                    var busGrid = BtnBus.Parent as Grid;
                    var pbTravel = busGrid.Children[4] as ProgressBar;
                    tbRow = busGrid.Children[0] as TextBox;
                    threadProgressBar.Bus = CurrentBus;
                    threadProgressBar.Obj = pbTravel;
                    threadProgressBar.Tb = tbRow;
                    string km = this.tbKms.Text;
                    int num = int.Parse(km);
                    Random rand = new Random();
                    double number=rand.Next(20, 50);
                    threadProgressBar.TimeOfDriving = (int)((num / number) * 6);
                    threadProgressBar.Number = num;
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
                    if (!bgWorker.IsBusy)
                    {
                        //TextBox bBus = (TextBox)sender;
                        //Bus bus = (Bus)bBus.DataContext;
                        
                        //List<Object> list = new List<object> { bus, c };

                        tbRow.Background = Brushes.Yellow;
                        tbRow.Opacity = 0.5;
                        bgWorker.RunWorkerAsync(threadProgressBar);
                    }
                    else
                        bgWorker.CancelAsync();
                   
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
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgw = sender as BackgroundWorker;
            ThreadProgressBar tgwBus = e.Argument as ThreadProgressBar;
            Bus bus = tgwBus.Bus as Bus;
            bus.Status = (int)BusStatus.MIDDLE_DRIVING;
            int number = (int)tgwBus.TimeOfDriving;
            for (int i = 1; i < number + 1; i++)
            {
                bgw.ReportProgress(i, tgwBus);
                System.Threading.Thread.Sleep(1000);
            }
            e.Result = tgwBus;
        }
        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ThreadProgressBar tpbBus = e.UserState as ThreadProgressBar;
            ProgressBar pbTravel = tpbBus.Obj as ProgressBar;
            pbTravel.Visibility = Visibility.Visible;
            pbTravel.Value = e.ProgressPercentage;
            pbTravel.Maximum = (int)tpbBus.TimeOfDriving;
        }
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ThreadProgressBar tpbBus = e.Result as ThreadProgressBar;
            Bus bus = tpbBus.Bus as Bus;
            ProgressBar pbTravel = tpbBus.Obj as ProgressBar;
            pbTravel.Visibility = Visibility.Hidden;
            bus.Status = (int)BusStatus.READY_TO_DRIVE;
            int num = tpbBus.Number;
            bus.FuelStatus -= num;
            bus.Maintenance += num;
            bus.TotalMileage += num;
            TextBox tbRow = tpbBus.Tb as TextBox;
            tbRow.Background = null;
            tbRow.Opacity = 1;
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
