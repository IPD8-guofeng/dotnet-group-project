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

namespace FrameWork
{
    /// <summary>
    /// Interaction logic for WatchList.xaml
    /// </summary>
    public partial class Watch : Window
    {
        Database db = new Database();
        public Watch()
        {
            InitializeComponent();
            try
            {
                List<WatchList> list = db.getWatchList();
                dgWatchList.ItemsSource = list;
                this.WindowStartupLocation = WindowStartupLocation.Manual;
                this.Left = 5;
                this.Top = 795;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: unable to fetch record ", "Database error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void dgWatchList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WatchList w = (WatchList)dgWatchList.SelectedItem;
            if (w != null)
            {
                StockChart c = new StockChart(w.StockTicker, "2016/01/01","2016/08/31");
                c.Owner = Application.Current.MainWindow;
                //c.WindowStartupLocation = WindowStartupLocation.Manual;
                c.Show();

                /*
                StockTrade s = new StockTrade(w);
                s.Owner = Application.Current.MainWindow;
                //c.WindowStartupLocation = WindowStartupLocation.Manual;
                s.Show();
                */
            }

        }
    }
}
