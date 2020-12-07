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


namespace dotNet5781_03B_4202_3855
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //tatic IObservable<Bus> busses = new 
        static ObservableCollection <Bus> busses = new ObservableCollection <Bus>();
        //private Bus currentDisplayBusLicenseNumber;
        public MainWindow()
        {
            busses = new ObservableCollection<Bus>();
            InitializeComponent();
            //cbBusLicenseNumber.ItemsSource = busses;
            //cbBusLicenseNumber.DisplayMemberPath = "LicenseNumber";
            //cbBusLicenseNumber.SelectedIndex = 0;
            initBus();
            lbBusDetails.ItemsSource = busses;
            //ShowBusLicenseNumber();
        }
        //private void ShowBusLicenseNumber()
        //{
        //    for (int i = 0; i < busses.Count; i++)
        //    {
        //        showOneBus(busses[i]);
        //        //ListBoxItem newItem = new ListBoxItem();
        //        //newItem.Content = busses[i].LicenseNumber;
        //        //lbBusDetails.Items.Add(newItem);
        //        //lbBusDetails.DataContext = busses[i];
        //        //currentDisplayBusLicenseNumber = busses[i];
        //        //DataContext = currentDisplayBusLicenseNumber;
        //        //lbBusDetails.DataContext = currentDisplayBusLicenseNumber.ToString();
        //    }
        //}
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
            fuel[0] = 1150;
            for (int i = 0; i < 10; i++)
            {
                busses.Add(new Bus(licenseNum[i], mainten[i], lastTreat[i], rand.Next(500000), fuel[i], dateBegin[i]));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBus wnd = new AddBus();
            bool? result = wnd.ShowDialog();
            if(result == true)
            {
                Bus newbus = wnd.AddedBus;
                foreach(Bus bus in busses)
                {
                    if (bus.LicenseNumber==newbus.LicenseNumber)
                        throw new ArgumentException("The bus already exists in the system.");
                }
                busses.Add(newbus);
                MessageBox.Show("The bus has been added successfuly.");
                //showOneBus(newbus);
            }
        }

        private void Click_Pick(object sender, RoutedEventArgs e)
        {
            var bBus = (sender as Button);
            Bus bus = (bBus.DataContext) as Bus;
            PickBusWindow exr = new PickBusWindow(bus);
            exr.Show();
            //if (result == true)
            //{
            //    //TODO
            //}
        }

        private void showOneBus(Bus newbus)
        {
            ListBoxItem newItem = new ListBoxItem();
            newItem.Content = newbus.LicenseNumber;
            lbBusDetails.Items.Add(newItem);
        }

        private void bRefueling_Click(object sender, RoutedEventArgs e)
        {
            Button bBus = (Button)sender;
            Bus bus = (Bus)bBus.DataContext;
            bus.FuelStatus = 0;
        }

        //private void bDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    if(e.ClickCount>=2)
        //    {
        //        Button bBus = (Button)sender;
        //        Bus bus = (Bus)bBus.DataContext;
        //        Details details = new Details(bus);
        //        details.ShowDialog();
        //    }
        //}

        //private void cbBusLicenseNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ShowBusLicenseNumber((cbBusLicenseNumber.SelectedValue as Bus).LicenseNumber);
        //}
    }
}
