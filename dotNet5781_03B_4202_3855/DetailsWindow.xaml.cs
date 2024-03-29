﻿using System;
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

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bus">current bus</param>
        /// <param name="tBus">current bus row</param>
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
                Bus bus = currentBus;
                BackgroundWorker bgWorker = new BackgroundWorker();
                bgWorker.DoWork += BgWorker_DoWork;
                bgWorker.ProgressChanged += BgWorker_ProgressChanged;
                bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
                bgWorker.WorkerSupportsCancellation = true;
                bgWorker.WorkerReportsProgress = true;
                BackgroundWorker bgWorkerRefuelingTimer = new BackgroundWorker();
                bgWorkerRefuelingTimer.DoWork += BgWorkerRefuelingTimer_DoWork;
                bgWorkerRefuelingTimer.ProgressChanged += BgWorkerRefuelingTimer_ProgressChanged;
                bgWorkerRefuelingTimer.RunWorkerCompleted += BgWorkerRefuelingTimer_RunWorkerCompleted;
                bgWorkerRefuelingTimer.WorkerReportsProgress = true;
                bgWorkerRefuelingTimer.WorkerSupportsCancellation = true;
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
                        bgWorkerRefuelingTimer.RunWorkerAsync();
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
            for (int i = 1; i < refuelCount + 1; i++)
            {
                bgw.ReportProgress(i, tpbBus);
                System.Threading.Thread.Sleep(1000);
            }
            e.Result = tpbBus;
        }
        /// <summary>
        ///  this is the second func of the thread that shows the progress change.
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
        /// this is the first func of the thread that starts running it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorkerRefuelingTimer_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorkerRefuelingTimer = sender as BackgroundWorker;
            for (int i = refuelCount; i >= 0; i--)
            {
                bgWorkerRefuelingTimer.ReportProgress(i);
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// this is the second func of the thread that shows the progress change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorkerRefuelingTimer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.RefuelTimer.Content = e.ProgressPercentage;
        }
        /// <summary>
        /// this is the third func of the thread that finish the refuelling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorkerRefuelingTimer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// this func is for the treatment timer, and it includes a back-ground-worker thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Treatment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnBus = (Button)sender;
                Bus bus = currentBus;
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

                if (bus.Status == (int)BusStatus.READY_TO_DRIVE || bus.Status == (int)BusStatus.NEED_REFUELING || bus.Status == (int)BusStatus.NEED_TREATMENT)
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
        }
        /// <summary>
        /// this is the first func of the thread that starts running it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorkerTreatmentTimer_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorkerTreatmentTimer = sender as BackgroundWorker;
            for (int i = treatmentCount; i >= 0; i--)
            {
                bgWorkerTreatmentTimer.ReportProgress(i);
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// this is the second func of the thread that shows the progress change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorkerTreatmentTimer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.lTimer.Content = e.ProgressPercentage;
        }
        /// <summary>
        /// this is the third func of the thread that finish the treatment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorkerTreatmentTimer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }


        /// <summary>
        ///  this is the first func of the treatment thread that starts running it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// this is the second func of the thread that shows the progress change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorkerTreatment_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ThreadProgressBar tpbBus = e.UserState as ThreadProgressBar;
            ProgressBar pbTreatment = tpbBus.Obj as ProgressBar;
            pbTreatment.Visibility = Visibility.Visible;
            pbTreatment.Value = e.ProgressPercentage;
            pbTreatment.Maximum = treatmentCount;
        }
        /// <summary>
        /// this is the third func of the thread that finish the treatment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// this func is for the window loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
        }
    }
}
