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
        private string ticker;
        public StockChart(string ticker, string startDateStr, string endDateStr)
        {
            InitializeComponent();
            this.ticker = ticker;
            tbTicker.Text = ticker;
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 5;
            this.Top = 50;
        }
        private void refreshChart()
        {

        }
        private void tbTicker_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObjectDataProvider data = (ObjectDataProvider)this.DataContext;
            StockChartModel model = (StockChartModel)data.Data;
            if (lbSuggestion.SelectedItem != null && dpStartDate.SelectedDate != null && dpEndDate.SelectedDate != null)
            {
                string selectedTicker = lbSuggestion.SelectedItem.ToString();
                model.SetStockTicker(selectedTicker,dpStartDate.SelectedDate.Value.ToShortDateString(), dpEndDate.SelectedDate.Value.ToShortDateString() );
                lblError.Visibility = Visibility.Hidden;
            }
            else
            {
                model.SetStockTicker("A","2016/01/01","2016/08/31");
                lblError.Content = "Error";
                lblError.Visibility = Visibility.Visible;

            }

            Plot.InvalidatePlot(true);

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
                //lbSuggestion.Visibility = Visibility.Hidden;
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
                    //lbSuggestion.Visibility = Visibility.Hidden;
                }
            }
        }
        private void lbSuggestion_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbSuggestion.SelectedIndex > -1)
            {
                tbTicker.Text = lbSuggestion.SelectedItem.ToString();
                ticker = tbTicker.Text;
                //lbSuggestion.Visibility = Visibility.Hidden;
            }
        }

        private void btnTrade_Click(object sender, RoutedEventArgs e)
        {
            StockTrade s = new StockTrade(ticker);
            //s.Owner = Application.Current.MainWindow;
            s.Show();
        }
    }
}
