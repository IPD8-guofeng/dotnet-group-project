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
            Initial();
        }
        public StockTrade(WatchList w)
        {
            InitializeComponent();
            Initial();
            tbTicker.Text = w.StockTicker;
            tbPrice.Text = w.ClosePrice.ToString();
        }
        public StockTrade(string ticker)
        {
            InitializeComponent();
            Initial();
            tbTicker.Text = ticker;
        }
        private void Initial()
        {
            string[] limitArray = { "Limit", "Market", "Stop" };
            foreach (string limit in limitArray)
            {
                cbLimit.Items.Add(limit);
            }
            cbLimit.SelectedIndex = 0;
            tbQuantity.Text = "100";
            lblBalance.Content = "Balance: $" + GlobalVariable.Balance.ToString();
        }

        /*****************  for stock ticker check option ********************************************/
        private void tbTicker_TextChanged(object sender, TextChangedEventArgs e)
        {

            lbSuggestion.Items.Clear();
            if (tbTicker.Text != "")
            {
                tbPrice.Text = GlobalVariable.db.getLatestPriceByTicker(tbTicker.Text).ToString();
                //List<Stock> namelist = CustomerGatewayObj.listShow(tbTicker.Text);
                List<string> tickerList = GlobalVariable.db.getTicker(tbTicker.Text);
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
            if (!GlobalVariable.db.IsValidTicker(tbTicker.Text)) return false;
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
        private void tryTradeTransaction(Transaction t)
        {
            if (t.ActionType == 1)
            {
                if (GlobalVariable.Balance >= (t.Quantity * t.Price))
                {
                    GlobalVariable.db.stockActionByTicker(t);
                    GlobalVariable.Balance -= t.Quantity * t.Price;
                    MessageBox.Show("Success bought the stock " + t.StockTicker + " " + t.Quantity + " share at $" + t.Price + ".\n"
                                     + "Total price: $" + t.Quantity * t.Price + "\n" + " Account balance: $" + GlobalVariable.Balance);
                }
                else
                {
                    MessageBox.Show("Your have $" + GlobalVariable.Balance + ", it needs $" + t.Quantity * t.Price);
                }
            }

            if (t.ActionType == 2)
            {
                List<StockOwned> sList = GlobalVariable.db.getAllStockOwned();
                if (sList != null)
                {
                    bool ableSell = false;
                    foreach (StockOwned s in sList)
                    {
                        if ((s.StockTicker == tbTicker.Text) && (s.Quantity >= int.Parse(tbQuantity.Text)))
                        {
                            ableSell = true;
                            break;
                        }
                    }
                    if (ableSell)
                    {
                        GlobalVariable.db.stockActionByTicker(t);
                        GlobalVariable.Balance += t.Quantity * t.Price;
                        MessageBox.Show("Success sold the stock " + t.StockTicker + " " + t.Quantity + " share at $" + t.Price + ".\n"
                                         + "Total price: $" + t.Quantity * t.Price + "\n" + " Account balance: $" + GlobalVariable.Balance);
                    }
                    else
                    {
                        MessageBox.Show("You do not have the stock or try to sell more than you owned");
                    }

                }
            }

        }
        private void btnTrade_Click(object sender, RoutedEventArgs e)
        {
            Transaction t =  new Transaction();
            if ((bool)rbBuy.IsChecked) { t = getTransObj(1); } // buy action type is 1
            else if ((bool)rbSell.IsChecked) { t = getTransObj(2); } // sell action type is 2
            else {
                MessageBox.Show("Your have to choose buy or sell" );
                return;
            }
            if (t != null)
            {
                double closePrice = GlobalVariable.db.getLatestPriceByTicker(t.StockTicker); // get latest close price
                if (closePrice > 0)
                {
                    switch (cbLimit.SelectedValue.ToString())
                    {
                        case "Limit":
                            if(t.ActionType == 1)
                            {
                                if (t.Price >= closePrice)
                                {
                                    t.Price = closePrice;
                                    tryTradeTransaction(t);
                                }
                                else
                                {
                                    MessageBox.Show("Your buy order is waiting to perform");
                                }
                            };
                            if (t.ActionType == 2)
                            {
                                if (t.Price <= closePrice)
                                {
                                    t.Price = closePrice;
                                    tryTradeTransaction(t);
                                }
                                else
                                {
                                    MessageBox.Show("Your sell order is waiting to perform");
                                }
                            }

                            break;
                        case "Market":
                            if (t.Price >= closePrice)
                            {
                                t.Price = closePrice;
                                tryTradeTransaction(t);
                            }
                            break;
                        case "Stop":
                            MessageBox.Show("Stop option is under construction"); break;
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


            lblBalance.Content = "Balance: $" + GlobalVariable.Balance.ToString();
        }


        private void cbLimit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLimit.SelectedItem.ToString() == "Market")
            {
                double closePrice = GlobalVariable.db.getLatestPriceByTicker(tbTicker.Text);
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

        private void rbSell_Checked(object sender, RoutedEventArgs e)
        {
            List<StockOwned> sList = GlobalVariable.db.getAllStockOwned();
            if ( sList.Count != 0 )
            {
                foreach (StockOwned s in sList)
                {
                    lbSuggestion.Items.Clear();
                    lbSuggestion.Items.Add(s.StockTicker);
                    tbQuantity.Text = s.Quantity.ToString();
                }
                lbSuggestion.Visibility = Visibility.Visible;
            }

        }
    }
}
