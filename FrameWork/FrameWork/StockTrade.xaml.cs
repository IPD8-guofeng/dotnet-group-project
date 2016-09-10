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
        Database db = new Database();
        const int defaultTransQuantity = 100;
        public StockTrade()
        {
            InitializeComponent();
            string[] limitArray = { "Market", "Limit", "Stop" };
            foreach (string limit in limitArray)
            {
                cbLimit.Items.Add(limit);
            }
            cbLimit.SelectedIndex = 0;
            tbQuantity.Text = defaultTransQuantity.ToString();
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

        /*****************  for stock ticker check option ********************************************/
        private void tbTicker_TextChanged(object sender, TextChangedEventArgs e)
        {

            lbSuggestion.Items.Clear();
            if (tbTicker.Text != "")
            {
                //List<Stock> namelist = CustomerGatewayObj.listShow(tbTicker.Text);
                List<string> tickerList = db.getTicker(tbTicker.Text);
                if (tickerList.Count > 0)
                {
                    lbSuggestion.Visibility = Visibility.Visible;
                    foreach (var obj in tickerList)
                    {
                        lbSuggestion.Items.Add(obj);
                    }
                }
            }
            else
            {
                lbSuggestion.Visibility = Visibility.Hidden;
            }
        }

        private void tbTicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                lbSuggestion.Focus();
            }
        }


        private void lbSuggestion_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (lbSuggestion.SelectedIndex == 0 && e.Key == Key.Up)
            {
                tbTicker.Focus();
            }
        }

        private void lbSuggestion_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbSuggestion.SelectedIndex > -1)
            {
                //???????? if (e.Key == Key.Enter)
                {
                    tbTicker.Text = lbSuggestion.SelectedItem.ToString();
                    lbSuggestion.Visibility = Visibility.Hidden;

                }
            }
        }
        private void lbSuggestion_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbSuggestion.SelectedIndex > -1)
            {
                tbTicker.Text = lbSuggestion.SelectedItem.ToString();
                lbSuggestion.Visibility = Visibility.Hidden;
            }
        }

        /********* End stock ticker check option **************************************************************/


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
                        Transaction t = new Transaction() { StockTicker = ticker, Quantity = quantity, Price = price, Action = 1 };
                        db.buyStockByticker(t);
                        MessageBox.Show("Success bought the stock " + ticker + " " + quantity + " share at $" + price + ".");
                        break;
                    case "Stop":
                        MessageBox.Show("stop function can not be use to buy stock"); break;
                    default:
                        MessageBox.Show("some error choosing Limit"); break;
                }
            }
            else
            {
                MessageBox.Show("Please check your input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void lblSell_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("sell function is not done yet");
        }


    }
}
