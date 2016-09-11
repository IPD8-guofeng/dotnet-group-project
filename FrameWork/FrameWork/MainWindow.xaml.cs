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

namespace FrameWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database db;
        const double defaultStartBalance = 100000;
        public MainWindow()
        {
            try
            {
                db = new Database();
                MessageBox.Show("database is connected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {

                MessageBox.Show("Fatal error: unable to connect to database", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                // TODO: Write details of the exception to log text file
                throw e;
            }
            InitializeComponent();
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            GlobalVariable.defaultStartBalance = defaultStartBalance;
            GlobalVariable.Balance = db.getBalance();
            //MessageBox.Show("balance: "+db.getBalance());
        }

        private void menuTrade_Click(object sender, RoutedEventArgs e)
        {
            // open Trade  window and put it in the center 
            StockTrade stockTradeWindow = new StockTrade();
            stockTradeWindow.Owner = Application.Current.MainWindow; 
            stockTradeWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            stockTradeWindow.Show();
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void menuChart_Click(object sender, RoutedEventArgs e)
        {
            // open Trade  window and put it in the center 
            StockChart stockChartWindow = new StockChart();
            stockChartWindow.Owner = Application.Current.MainWindow;
            stockChartWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            stockChartWindow.Show();
        }
    }
}
