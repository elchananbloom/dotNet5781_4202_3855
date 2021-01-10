using BO;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for BusDetailsWindow.xaml
    /// </summary>
    public partial class BusDetailsWindow : Window
    {
        public BusBO BusBO { get; }
        public TextBox TbBus { get; }
        public BusDetailsWindow(BusBO busBO, TextBox tbBus)
        {
            BusBO = busBO;
            TbBus = tbBus;
            InitializeComponent();
            dateBeginTextBox.Content = BusBO.StartOfWork.ToString();
            fuelStatusTextBox.Content = BusBO.Fuel.ToString();
            lastTreatmentTextBox.Content = BusBO.LastTreatment.ToString();
            licenseNumberTextBox.Content = BusBO.LicenseNumber;
            maintenanceTextBox.Content = BusBO.Maintnance.ToString();
            totalMileageTextBox.Content = BusBO.TotalKms.ToString();
        }
        public BusDetailsWindow()
        {
        }
    }
}
