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
    /// Interaction logic for PortfolioWin.xaml
    /// </summary>
    public partial class PortfolioWin : Window
    {
        Database db = new Database();
        int currentPortId;
        List<PortTransaction> portfolioList = new List<PortTransaction>();
        List<PortTransaction> transactionList = new List<PortTransaction>();

        public PortfolioWin()
        {

            InitializeComponent();
            //get all portfolio name from Table "Portfolio", assign to listbox "lbPortfolio"
            //get the default portfolio name and assigne it to the Title "lblPortName"

            getPortfolio();
            currentPortId = 1;
            lblPortName.Content = lbPortfolio.Items.GetItemAt(currentPortId - 1);

            //get all transcation data for default portfolio from Table"Transcation", 
            //assign to data grid "dgTranscation"
            dgTranscation.ItemsSource = transactionList;

            //get sumarized transcation data for default portfolio from Table"Transcation", 
            //assign to data grid "dgPortfolio"
            dgPortfolio.ItemsSource = portfolioList;

            //get all Company data from Table"Company", 
            //assign to combox "cmbStock"
            getStockNames();
        }
        private void getPortfolio()
        {
            List<Portfolio> list = null;
            try
            {
                list = db.GetAllPortfolios();
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to fetch records from database" + e.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //throw;
            }
            //dgToDoItems.Items.Refresh();
            foreach (Portfolio p in list)
            {
                lbPortfolio.Items.Add(p.Name);
            }
        }
        private void getStockNames()
        {
            List<string> list = null;
            try
            {
                list = db.GetAllStockNames();
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to fetch records from database" + e.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //throw;
            }
            //dgToDoItems.Items.Refresh();
            foreach (string p in list)
            {
                cmbStock.Items.Add(p);
            }
        }

        private void btCreatPortfolio_Click(object sender, RoutedEventArgs e)
        {
            string name;
            name = tbPortname.Text;
            //the portfolio name must be more than 6 characters
            if (name.Length < 6)
            {
                MessageBox.Show("the portfolio name must be more than 6 characters", "Input Error",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            //the portfolio name should not be existed already
            if (lbPortfolio.Items.Contains(name))
            {
                MessageBox.Show("the portfolio name  already existed!", "Input Error",
               MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            //insert it to table "Portfolio"
            db.AddPortfolio(name);
            //get the portId for new portfolio, assign to the "currentPortId",
            int id = db.PortIdByName(name);
            currentPortId = id;

            //clear content of "dgPortfolio","dgTranscation"
            lblPortName.Content = name;
            lbPortfolio.Items.Add(name);
        }

        private void btCashDeposit_Click(object sender, RoutedEventArgs e)
        {
            //Cash Deposit
            PortTransaction trans = new PortTransaction();
            CashTransaction dialog = new CashTransaction("Cash Deposit");
            if (dialog.ShowDialog() == false)
            { // if user pressed "Cancel"
                //string valueEntered = dialog.ValueEntered;
                return;
            }
            trans = dialog.trans;
            trans.portId = currentPortId;
            trans.Symbol = ""
            trans.Type = TransType.Deposit;
            trans.Share = 1;

            //insert the data to Table "Transcation"
            db.AddPortTransaction(trans);

            //update the transcation data, dgTranscation
            transactionList.Add(trans);
            dgTranscation.Items.Refresh();

            //update the portfolio data, dgPortfolio
            portfolioList.Add(trans);
            dgPortfolio.Items.Refresh();

        }

        private void btCashWithdraw_Click(object sender, RoutedEventArgs e)
        {
            //Cash Withdraw
            PortTransaction trans = new PortTransaction();
            CashTransaction dialog = new CashTransaction("Cash Withdraw");
            if (dialog.ShowDialog() == false)
            { // if user pressed "Cancel"
                //string valueEntered = dialog.ValueEntered;
                return;
            }
            trans = dialog.trans;
            trans.portId = currentPortId;
            trans.Type = TransType.Withdraw;
            trans.Share = -1;

            //insert the data to Table "Transcation"
            db.AddPortTransaction(trans);

            //update the transcation data, dgTranscation
            transactionList.Add(trans);
            dgTranscation.Items.Refresh();

            //update the portfolio data, dgPortfolio
            portfolioList.Add(trans);
            dgPortfolio.Items.Refresh();

        }

        private void btAddTrans_Click(object sender, RoutedEventArgs e)
        {
            PortTransaction trans = new PortTransaction();

            //check if an stock ticker is selected
            ComboBoxItem cmbItem = (ComboBoxItem)cmbStock.SelectedItem;
            string ticker = cmbItem.Content.ToString();

            if (ticker == "")
            {
                MessageBox.Show("Please select a sotck!", "Input Error",
              MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            string[] companyInfo = ticker.ToString().Split('|');

            StockTransaction dialog = new StockTransaction(string.Join(" | ", companyInfo));
            if (dialog.ShowDialog() == false)
            { // if user pressed "Cancel"
                //string valueEntered = dialog.ValueEntered;
                return;
            }
            // assign the input value to Transcation obj
            trans = dialog.trans;

            trans.portId = currentPortId;
            trans.Symbol = companyInfo[0];
            //trans.Name = companyInfo[1];

            //insert the data to Table "Transcation"
            db.AddPortTransaction(trans);


            //update the transcation data, dgTranscation
            transactionList.Add(trans);
            dgTranscation.Items.Refresh();

            //update the portfolio data, dgPortfolio
            portfolioList.Add(trans);
            dgPortfolio.Items.Refresh();

        }

        private void btAddToPort_Click(object sender, RoutedEventArgs e)
        {
            PortTransaction trans = new PortTransaction();

            //check if an stock ticker is selected
            ComboBoxItem cmbItem = (ComboBoxItem)cmbStock.SelectedItem;
            string ticker = cmbItem.Content.ToString();

            if (ticker == "")
            {
                MessageBox.Show("Please select a sotck!", "Input Error",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            string[] companyInfo = ticker.ToString().Split('|');

            trans.portId = currentPortId;
            trans.Symbol = companyInfo[0];
            //trans.Name = companyInfo[1];

            //insert the data to Table "Transcation"
            db.AddPortTransaction(trans);

            //update the transcation data, dgTranscation
            transactionList.Add(trans);
            dgTranscation.Items.Refresh();

            //update the portfolio data, dgPortfolio
            portfolioList.Add(trans);
            dgPortfolio.Items.Refresh();

            //insert the data to Table "Transcation

        }

        private void lbPortfolio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = lbPortfolio.SelectedItem.ToString();
            if (item == "") return;
            lblPortName.Content = item;

            //find the portid from Table Portfolio and assigned to currentPortId;
            int id = db.PortIdByName(item);
            //MessageBox.Show("" + id, "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
            if (id == 0)
            {
                MessageBox.Show("Cannot get the Portfolil Id!", "Database Error",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            currentPortId = id;
        }
    }
}
