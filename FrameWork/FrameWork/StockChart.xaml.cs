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
    /// Interaction logic for StockChart.xaml
    /// </summary>
    public partial class StockChart : Window
    {
        Database db = new Database();
        public StockChart()
        {
            InitializeComponent();
        }
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

    }
}
