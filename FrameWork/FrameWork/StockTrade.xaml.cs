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
            string[] limitArray = {  "Limit", "Market", "Stop" };
            foreach (string limit in limitArray)
            {
                cbLimit.Items.Add(limit);
            }
            cbLimit.SelectedIndex = 0;
            tbQuantity.Text = defaultTransQuantity.ToString();
        }

        /*****************  for stock ticker check option ********************************************/
        private void tbTicker_TextChanged(object sender, TextChangedEventArgs e)
        {

            lbSuggestion.Items.Clear();
            if (tbTicker.Text != "")
            {
                tbPrice.Text = db.getLatestPriceByTicker(tbTicker.Text).ToString();
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
        // check all the input are valid 
        private bool IsValidFormatInput()
        {
            if (!db.IsValidTicker(tbTicker.Text)) return false;
            int quantity;
            if (!int.TryParse(tbQuantity.Text, out quantity)) return false;
            if (quantity <= 0) return false;
            double price;
            if (!double.TryParse(tbPrice.Text, out price)) return false;
            if (price <= 0) return false;
            return true;
        }
        private Transaction getTransObj(int actionType)
        {
            if (IsValidFormatInput())
            {
                string ticker = tbTicker.Text;
                int quantity = int.Parse(tbQuantity.Text);
                string limit = cbLimit.SelectedValue.ToString();
                double price = double.Parse(tbPrice.Text);
                Transaction t = new Transaction() { StockTicker = ticker, Quantity = quantity, Price = price, ActionType = actionType };
                return t;
            }
            else
            {
                return null;
            }
        }
        private void tryBuyTransaction(Transaction t)
        {
            if (GlobalVariable.Balance >= (t.Quantity * t.Price))
            {
                db.stockActionByTicker(t);
                GlobalVariable.Balance -= t.Quantity * t.Price;
                MessageBox.Show("Success bought the stock " + t.StockTicker + " " + t.Quantity + " share at $" + t.Price + ".\n"
                                 + "Total price: $" + t.Quantity * t.Price +"\n" + " Account balance: $" + GlobalVariable.Balance);
            }
            else
            {
                MessageBox.Show("Your have $" + GlobalVariable.Balance + ", it needs $" + t.Quantity * t.Price);
            }
        }
        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            Transaction t = getTransObj(1);  // buy action type is 1
            if (t != null)
            {
                double closePrice = db.getLatestPriceByTicker(t.StockTicker); // get latest close price
                if (closePrice > 0)
                {
                    switch (cbLimit.SelectedValue.ToString())
                    {
                        case "Limit":
                            if (t.Price >= closePrice)
                            {
                                t.Price = closePrice;
                                tryBuyTransaction(t);
                            }
                            else
                            {
                                MessageBox.Show("Your buy order is waiting to perform");
                            }
                            break;
                        case "Market":
                            if (t.Price >= closePrice)
                            {
                                t.Price = closePrice;
                                tryBuyTransaction(t);
                            }
                            break;
                        case "Stop":
                            MessageBox.Show("Stop option can not be use to buy stock"); break;
                        default:
                            MessageBox.Show("some error choosing Limit"); break;
                    }
                }
                else
                {
                    MessageBox.Show("can not find stock market price");
                }
            }
            else
            {
                MessageBox.Show("Please check your input format or value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("sell function is not done yet");
        }

        private void cbLimit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLimit.SelectedItem.ToString() == "Market")
            {
                double closePrice = db.getLatestPriceByTicker(tbTicker.Text);
                if (closePrice > 0)
                {
                    tbPrice.Text = closePrice.ToString();
                    tbPrice.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("can not find stock market price");
                }

            }
            else
            {
                tbPrice.IsEnabled = true;
            }
        }
    }
}
