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
    /// 
    public partial class MainWindow : Window
    {

        Database db;
        //public static StockTrade tradeFirst = new StockTrade();
        //public static StockTrade tradeSecond = new StockTrade();
        public MainWindow()
        {
            try
            {
                db = new Database();
               
            }
            catch (Exception e)
            {

                MessageBox.Show("Fatal error: unable to connect to database", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                // TODO: Write details of the exception to log text file
                throw e;
            }
            InitializeComponent();
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            GlobalVariable.Balance = db.getBalance();
            this.Show();

            

        }

        private void menuTrade_Click(object sender, RoutedEventArgs e)
        {
            // open Trade  window and put it in the center 
            StockTrade s = new StockTrade();
            s.Owner = Application.Current.MainWindow;
            //s.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            s.Show();
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void menuChart_Click(object sender, RoutedEventArgs e)
        {
            // open Trade  window and put it in the center 
            StockChart c = new StockChart("A","2016/01/01","2016/08/31");
            c.Owner = Application.Current.MainWindow;
           // c.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            c.Show();
        }

        private void menuWatchList_Click(object sender, RoutedEventArgs e)
        {
            Watch w = new Watch();
            w.Owner = Application.Current.MainWindow;
            //w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            w.Show();
        }

        private void menuPortfolio_Click(object sender, RoutedEventArgs e)
        {
            PortfolioWin w = new PortfolioWin();
            w.Owner = Application.Current.MainWindow;
            //w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            w.Show();
        }

        private void menuCloseAllWindows_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window item in App.Current.Windows)
            {
                if (item != this)
                    item.Close();
            }
        }

        private void menuOpenAllWindows_Click(object sender, RoutedEventArgs e)
        {
            StockTrade.count = 0;
            StockTrade trade1 = new StockTrade();
            trade1.Owner = Application.Current.MainWindow;
            trade1.Show();

            StockTrade trade2 = new StockTrade();
            trade2.Owner = Application.Current.MainWindow;
            trade2.Show();

            StockChart chart = new StockChart("A", "2016/01/01", "2016/08/31");
            chart.Owner = Application.Current.MainWindow;
            chart.Show();

            Watch watch = new Watch();
            watch.Owner = Application.Current.MainWindow;
            watch.Show();

            PortfolioWin portfolioWin = new PortfolioWin();
            portfolioWin.Owner = Application.Current.MainWindow;
            portfolioWin.Show();
        }
    }
}
