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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace dotNet5781_03B_4202_3855
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ObservableCollection<Bus> busses = new ObservableCollection<Bus>();
        private int refuelCount = 12;
        ProgressBar progressBar = new ProgressBar();

        /// <summary>
        /// ctor.
        /// </summary>
        public MainWindow()
        {
            busses = new ObservableCollection<Bus>();
            InitializeComponent();
            initBus();
            lbBusDetails.ItemsSource = busses;
        }
     
        /// <summary>
        /// initializing 10 busses
        /// </summary>
        private static void initBus()
        {
            string[] licenseNum = { "5632357", "57643276", "9853435", "56623267", "8534356", "5452537", "57342125", "56745257", "9743247", "2378635" };
            int[] mainten = new int[10];
            Random rand = new Random();
            for (int i = 0; i < 9; i++)
            {
                mainten[i] = rand.Next(20000);
            }
            mainten[9] = 19950;
            DateTime[] lastTreat = { new DateTime(2020, 9, 23), new DateTime(2020, 7, 13), new DateTime(2020, 11, 9), new DateTime(2020, 10, 14), new DateTime(2020, 1, 8), new DateTime(2019, 12, 22),
                new DateTime(2018, 6, 15),new DateTime(2020, 4, 27),new DateTime(2020, 8, 29),new DateTime(2020, 7, 4) };
            DateTime[] dateBegin = { new DateTime(1996, 5, 7), new DateTime(2019, 12, 17), new DateTime(2015, 3, 26), new DateTime(2020, 5, 7), new DateTime(2010, 2, 16), new DateTime(2007, 6, 11),
                new DateTime(2018, 3, 29),new DateTime(2019, 8, 23),new DateTime(2002, 4, 15),new DateTime(1999, 5, 17) };
            int[] fuel = new int[10];
            for (int i = 1; i < 10; i++)
            {
                fuel[i] = rand.Next(1000);
            }
            fuel[0] = 50;
            for (int i = 0; i < 10; i++)
            {
                busses.Add(new Bus(licenseNum[i], mainten[i], lastTreat[i], rand.Next(500000), fuel[i], dateBegin[i]));
            }
        }

        /// <summary>
        /// this func is for oppening the addbus window and update the new bus in list. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow wnd = new AddBusWindow();
            bool? result = wnd.ShowDialog();
            if (result == true)
            {
                Bus newbus = wnd.AddedBus;
                foreach (Bus bus in busses)
                {
                    if (bus.LicenseNumber == newbus.LicenseNumber)
                        throw new ArgumentException("The bus already exists in the system.");
                }
                busses.Add(newbus);
                MessageBox.Show("The bus has been added successfuly.");
            }
        }
        /// <summary>
        /// this func is for the refuling button, and it includes a back-ground-worker thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bRefueling_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnBus = (Button)sender;
                Bus bus = (Bus)btnBus.DataContext;
                BackgroundWorker bgWorker = new BackgroundWorker();
                bgWorker.DoWork += BgWorker_DoWork;
                bgWorker.ProgressChanged += BgWorker_ProgressChanged;
                bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
                bgWorker.WorkerSupportsCancellation = true;
                bgWorker.WorkerReportsProgress = true;
                ThreadProgressBar threadProgressBar = new ThreadProgressBar();
                TextBox tbRow = new TextBox();

                if (bus.Status == (int)BusStatus.READY_TO_DRIVE || bus.Status == (int)BusStatus.NEED_REFUELING)
                {
                    if (!bgWorker.IsBusy)
                    {

                        var busGrid = btnBus.Parent as Grid;
                        var pbRefuel = busGrid.Children[3] as ProgressBar;
                        tbRow = busGrid.Children[0] as TextBox;
                        threadProgressBar.Bus = bus;
                        threadProgressBar.Obj = pbRefuel;
                        threadProgressBar.Tb = tbRow;

                        tbRow.Background = Brushes.Red;
                        tbRow.Opacity = 0.5;
                        bgWorker.RunWorkerAsync(threadProgressBar);
                    }
                    else
                        bgWorker.CancelAsync();
                }
                else
                    throw new ArgumentException("The bus can not be refueled.");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        /// <summary>
        /// this is the first func of the thread that starts running it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgw = sender as BackgroundWorker;
            ThreadProgressBar tpbBus = e.Argument as ThreadProgressBar;
            Bus bus = tpbBus.Bus as Bus;
            bus.Status = (int)BusStatus.IN_REFUELING;
            for (int i = 1; i <= refuelCount; i++)
            {
                bgw.ReportProgress(i, tpbBus);
                System.Threading.Thread.Sleep(1000);
            }
            e.Result = tpbBus;
        }
        /// <summary>
        /// this is the second func of the thread that shows the progress change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ThreadProgressBar tpbBus = e.UserState as ThreadProgressBar;
            ProgressBar pbRefuel = tpbBus.Obj as ProgressBar;
            pbRefuel.Visibility = Visibility.Visible;
            pbRefuel.Value = e.ProgressPercentage;
            pbRefuel.Maximum = refuelCount;
        }
        /// <summary>
        /// this is the third func of the thread that finish the refuelling. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ThreadProgressBar tpbBus = e.Result as ThreadProgressBar;
            Bus bus = tpbBus.Bus as Bus;
            ProgressBar pbRefuel = tpbBus.Obj as ProgressBar;
            bus.FuelStatus = 1200;
            pbRefuel.Visibility = Visibility.Hidden;
            bus.Status = (int)BusStatus.READY_TO_DRIVE;
            TextBox tbRow = tpbBus.Tb as TextBox;
            tbRow.Background = null;
            tbRow.Opacity = 1;
        }
        /// <summary>
        /// this func openes the travel window for the driving of the bus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Pick(object sender, RoutedEventArgs e)
        {
            try
            {
                var bBus = (sender as Button);
                Bus bus = (bBus.DataContext) as Bus;
                if (bus.Status == (int)BusStatus.READY_TO_DRIVE)
                {
                    TravelWindow exr = new TravelWindow(bus, bBus);
                    exr.ShowDialog();
                }
                else
                    throw new ArgumentException("The bus is not ready to be used.");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        /// <summary>
        /// this func is for the showing the bus details that openes by pressing doubleclick on the license number. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox tbBus = sender as TextBox;
            Bus bus = (Bus)tbBus.DataContext;
            DetailsWindow details = new DetailsWindow(bus, tbBus);
            details.ShowDialog();
        }
    }
}
