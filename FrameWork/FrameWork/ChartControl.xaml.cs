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
    /// Interaction logic for ChartControl.xaml
    /// </summary>
    public partial class ChartControl : Window
    {
        Database db = new Database();
        public ChartControl()
        {
            InitializeComponent();
            string tickerText = "";
            List<string> list = db.GetStockTickerFromPriceTable();
            foreach (string s in list){
                tickerText += (s+"\n");
            }
            tbTicker.Text = tickerText ;

            StockChart c = new StockChart();
            c.Show();
        }
    }
}
