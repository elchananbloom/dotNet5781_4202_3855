using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private Bus currentBus;
        private TextBox tbBus;
        public Bus CurrentBus { get => currentBus; }
        public TextBox TbBus { get => tbBus; set => tbBus = value; }
        BackgroundWorker bgWorker = new BackgroundWorker();
        private int refuelCount = 12;
        private int treatmentCount = 144;

        public DetailsWindow(Bus bus,TextBox tBus)
        {
            currentBus = bus;
            tbBus = tBus;
            InitializeComponent();
            dateBeginTextBox.Content = currentBus.DateBegin.ToString();
            fuelStatusTextBox.Content = currentBus.FuelStatus.ToString();
            lastTreatmentTextBox.Content = currentBus.LastTreatment.ToString();
            licenseNumberTextBox.Content = currentBus.LicenseNumber;
            maintenanceTextBox.Content = currentBus.Maintenance.ToString();
            totalMileageTextBox.Content = currentBus.TotalMileage.ToString();
            
            //ListBoxItem newItem = new ListBoxItem();
            //newItem.Content = el.ToString();
            //lbDetails.Items.Add(newItem);
            //DataContext = el;
            //lbDetails.DataContext = el.ToString();
        }

       

        private void bRefueling_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnBus = (Button)sender;
                Bus bus = currentBus;
                //Bus bus1 = (Bus)bBus.DataContext;
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

                        var tbGrid = tbBus.Parent as Grid;
                        var pbRefuel = tbGrid.Children[3] as ProgressBar;
                        tbRow = tbGrid.Children[0] as TextBox;
                        threadProgressBar.Bus = bus;
                        threadProgressBar.Obj = pbRefuel;
                        threadProgressBar.Tb = tbRow;

                        tbRow.Background = Brushes.Red;
                        tbRow.Opacity = 0.5;
                        this.Close();
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
            //currentBus.FuelStatus = 1200;
        }
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgw = sender as BackgroundWorker;
            ThreadProgressBar tpbBus = e.Argument as ThreadProgressBar;
            Bus bus = tpbBus.Bus as Bus;
            bus.Status = (int)BusStatus.IN_REFUELING;
            for (int i = 1; i < refuelCount + 1; i++)
            {
                bgw.ReportProgress(i, tpbBus);
                System.Threading.Thread.Sleep(1000);
            }
            e.Result = tpbBus;
        }
        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ThreadProgressBar tpbBus = e.UserState as ThreadProgressBar;
            ProgressBar pbRefuel = tpbBus.Obj as ProgressBar;
            pbRefuel.Visibility = Visibility.Visible;
            pbRefuel.Value = e.ProgressPercentage;
            pbRefuel.Maximum = refuelCount;
        }
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
        private void Treatment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnBus = (Button)sender;
                Bus bus = currentBus;
                //Bus bus1 = (Bus)bBus.DataContext;
                BackgroundWorker bgWorkerTreatment = new BackgroundWorker();
                BackgroundWorker bgWorkerTreatmentTimer = new BackgroundWorker();
                bgWorkerTreatmentTimer.DoWork += BgWorkerTreatmentTimer_DoWork;
                bgWorkerTreatmentTimer.ProgressChanged += BgWorkerTreatmentTimer_ProgressChanged;
                bgWorkerTreatmentTimer.RunWorkerCompleted += BgWorkerTreatmentTimer_RunWorkerCompleted;
                bgWorkerTreatmentTimer.WorkerReportsProgress = true;
                bgWorkerTreatmentTimer.WorkerSupportsCancellation = true;
                bgWorkerTreatment.DoWork += BgWorkerTreatment_DoWork;
                bgWorkerTreatment.ProgressChanged += BgWorkerTreatment_ProgressChanged;
                bgWorkerTreatment.RunWorkerCompleted += BgWorkerTreatment_RunWorkerCompleted;
                bgWorkerTreatment.WorkerSupportsCancellation = true;
                bgWorkerTreatment.WorkerReportsProgress = true;
                ThreadProgressBar threadProgressBar = new ThreadProgressBar();
                TextBox tbRow = new TextBox();

                if (bus.Status == (int)BusStatus.READY_TO_DRIVE || bus.Status == (int)BusStatus.NEED_REFUELING||bus.Status==(int)BusStatus.NEED_TREATMENT)
                {
                    if (!bgWorkerTreatment.IsBusy)
                    {

                        var tbGrid = tbBus.Parent as Grid;
                        var pbTreatment = tbGrid.Children[5] as ProgressBar;
                        tbRow = tbGrid.Children[0] as TextBox;
                        threadProgressBar.Bus = bus;
                        threadProgressBar.Obj = pbTreatment;
                        threadProgressBar.Tb = tbRow;

                        tbRow.Background = Brushes.Brown;
                        tbRow.Opacity = 0.5;
                        bgWorkerTreatmentTimer.RunWorkerAsync();
                        bgWorkerTreatment.RunWorkerAsync(threadProgressBar);
                    }
                    else
                        bgWorkerTreatment.CancelAsync();
                }
                else
                    throw new ArgumentException("The bus can not be treated.");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            //currentBus.FuelStatus = 1200;
            //currentBus.LastTreatment = DateTime.Now;
            //currentBus.Maintenance = 0;
            //currentBus.Status = 1;
        }

        private void BgWorkerTreatmentTimer_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorkerTreatmentTimer = sender as BackgroundWorker;
            for (int i=treatmentCount;i>=0;i--)
            {
                bgWorkerTreatmentTimer.ReportProgress(i);
                Thread.Sleep(1000);
            }
        }

        private void BgWorkerTreatmentTimer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.lTimer.Content = e.ProgressPercentage;
        }

        private void BgWorkerTreatmentTimer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void BgWorkerTreatment_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgw = sender as BackgroundWorker;
            ThreadProgressBar tpbBus = e.Argument as ThreadProgressBar;
            Bus bus = tpbBus.Bus as Bus;
            bus.Status = (int)BusStatus.IN_TREATMENT;
            for (int i = 1; i < treatmentCount + 1; i++)
            {
                bgw.ReportProgress(i, tpbBus);
                System.Threading.Thread.Sleep(1000);
            }
            e.Result = tpbBus;
        }

        private void BgWorkerTreatment_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ThreadProgressBar tpbBus = e.UserState as ThreadProgressBar;
            ProgressBar pbTreatment = tpbBus.Obj as ProgressBar;
            pbTreatment.Visibility = Visibility.Visible;
            pbTreatment.Value = e.ProgressPercentage;
            pbTreatment.Maximum = treatmentCount;
        }

        private void BgWorkerTreatment_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ThreadProgressBar tpbBus = e.Result as ThreadProgressBar;
            Bus bus = tpbBus.Bus as Bus;
            ProgressBar pbTreatment = tpbBus.Obj as ProgressBar;
            bus.FuelStatus = 1200;
            bus.LastTreatment = DateTime.Now;
            bus.Maintenance = 0;
            pbTreatment.Visibility = Visibility.Hidden;
            bus.Status = (int)BusStatus.READY_TO_DRIVE;
            TextBox tbRow = tpbBus.Tb as TextBox;
            tbRow.Background = null;
            tbRow.Opacity = 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }
    }
}
