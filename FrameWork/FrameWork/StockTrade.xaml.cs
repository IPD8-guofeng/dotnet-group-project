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
    /// Interaction logic for StockTrade.xaml
    /// </summary>
    public partial class StockTrade : Window
    {
        public StockTrade()
        {
            InitializeComponent();
            string[] limitArray = {"Market","Limit","Stop"};
            foreach (string limit in limitArray)
            {
                cbLimit.Items.Add(limit);
            }
            cbLimit.SelectedIndex = 0;
        }

        private void tbSearch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("search function is not done yet");
            //tbPrice.Text = getCurrentPrice(tbticker.Text);
        }

        private bool IsValidTradeInput()
        {
            if (!IsValidTicker(tbTicker.Text)) return false;
            int quantity;
            if (!int.TryParse(tbQuantity.Text, out quantity)) return false;
            double price;
            if (!double.TryParse(tbPrice.Text, out price)) return false;
            return true;
        }
        

        // Todo check stock ticker is in the database StockTrade
        private bool IsValidTicker(string stockTicker)
        {
            MessageBox.Show("IsValidTicker is in progress");
            return true;
        }
        private void lblBuy_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("buy function is in progress");
            if (IsValidTradeInput())
            {
                string ticker = tbTicker.Text;
                int quantity = int.Parse(tbQuantity.Text);
                string limit = cbLimit.SelectedValue.ToString();
                double price = double.Parse(tbPrice.Text);
                switch (limit)
                {
                    case "Market":
                    case "Limit":
                        //buyStockByticker(ticker,price,quantity,limit,action);
                        MessageBox.Show("Success bought the stock " + ticker + " " + quantity + " share at $" + price + ".");
                        break;
                    case "Stop":
                        MessageBox.Show("stop function can not be use to buy stock"); break;
                    default:
                        MessageBox.Show("some error choosing Limit"); break;
                }
            }
           
        }

        private void lblSell_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("sell function is not done yet");
        }
    }
}
