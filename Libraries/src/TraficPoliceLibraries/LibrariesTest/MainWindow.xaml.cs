using System.Windows;

namespace LibrariesTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private TrafficPoliceLibraries.Class1 VIN_LIB = new TrafficPoliceLibraries.Class1();
        private REG_MARK_LIB.Class1 REG_MARK_LIB = new REG_MARK_LIB.Class1();

        private string[] _invalidVINs = new string[]
        {
            "KLAVA6928XdB203010",
            "KLAVA692",
            "KLAVA6928XB203011",
            "KLAVQ6928XB203011"
        };

        private string[] _validVINs = new string[]
        {
            "JH4KB16535L011820",
            "KLAJB82Z2XK338143",
            "KLAVA6928XB203010"
        };

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (string vin in _invalidVINs)
            {
                MainTextBlock.Text += $"{VIN_LIB.CheckVIN(vin)} ";
            }

            MainTextBlock.Text += "\r";

            foreach (string vin in _validVINs)
            {
                MainTextBlock.Text += $"{VIN_LIB.CheckVIN(vin)} ";
            }
        }
    }
}
