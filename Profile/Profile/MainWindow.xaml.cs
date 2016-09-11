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

namespace Profile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int currentPortId;
        List<Transcation> portfolioList=new List<Transcation>();
        List<Transcation> transactionList = new List<Transcation>();

        public MainWindow()
        {
            InitializeComponent();
            //get all portfolio name from Table "Portfolio", assign to listbox "lbPortfolio"
            //get the default portfolio name and assigne it to the Title "lblPortName"
            lblPortName.Content = "My Portfolio";
            currentPortId = 1;

            //get all transcation data for default portfolio from Table"Transcation", 
            //assign to data grid "dgTranscation"
            dgTranscation.ItemsSource = transactionList;

            //get sumarized transcation data for default portfolio from Table"Transcation", 
            //assign to data grid "dgPortfolio"
            dgPortfolio.ItemsSource = portfolioList;

            //get all Company data from Table"Company", 
            //assign to combox "cmbStock"
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
            //get the portId for new portfolio, assign to the "currentPortId",
            //clear content of "dgPortfolio","dgTranscation"
            lblPortName.Content = name;
            lbPortfolio.Items.Add(name);
        }

        private void btCashDeposit_Click(object sender, RoutedEventArgs e)
        {
            //Cash Deposit
            Transcation trans = new Transcation();
            CashTranscation dialog = new CashTranscation("Cash Deposit");
            if (dialog.ShowDialog() == false)
            { // if user pressed "Cancel"
                //string valueEntered = dialog.ValueEntered;
                return;
            }
            trans = dialog.trans;
            trans.portId = currentPortId;
            trans.Type = TransType.Deposit;
            trans.Share = 1;

            //update the transcation data, dgTranscation
            transactionList.Add(trans);
            dgTranscation.Items.Refresh();

            //update the portfolio data, dgPortfolio
            portfolioList.Add(trans);
            dgPortfolio.Items.Refresh();

            //insert the data to Table "Transcation"


        }

        private void btCashWithdraw_Click(object sender, RoutedEventArgs e)
        {
            //Cash Withdraw
            Transcation trans = new Transcation();
            CashTranscation dialog = new CashTranscation("Cash Withdraw");
            if (dialog.ShowDialog() == false)
            { // if user pressed "Cancel"
                //string valueEntered = dialog.ValueEntered;
                return;
            }
            trans = dialog.trans;
            trans.portId = currentPortId;
            trans.Type = TransType.Withdraw;
            trans.Share = -1;

            //update the transcation data, dgTranscation
            transactionList.Add(trans);
            dgTranscation.Items.Refresh();

            //update the portfolio data, dgPortfolio
            portfolioList.Add(trans);
            dgPortfolio.Items.Refresh();

            //insert the data to Table "Transcation"

        }

        private void btAddTrans_Click(object sender, RoutedEventArgs e)
        {
            Transcation trans = new Transcation();

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
           
            StockTranscation dialog = new StockTranscation(string.Join(" | ", companyInfo));
            if (dialog.ShowDialog() == false)
            { // if user pressed "Cancel"
                //string valueEntered = dialog.ValueEntered;
                return;
            }
            // assign the input value to Transcation obj
            trans = dialog.trans;

            trans.portId = currentPortId;
            trans.Symbol = companyInfo[0];
            trans.Name = companyInfo[1];

            //update the transcation data, dgTranscation
            transactionList.Add(trans);
            dgTranscation.Items.Refresh();

            //update the portfolio data, dgPortfolio
            portfolioList.Add(trans);
            dgPortfolio.Items.Refresh();

            //insert the data to Table "Transcation
        }

        private void btAddToPort_Click(object sender, RoutedEventArgs e)
        {
            Transcation trans = new Transcation();

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
            trans.Name = companyInfo[1];

            //update the transcation data, dgTranscation
            transactionList.Add(trans);
            dgTranscation.Items.Refresh();

            //update the portfolio data, dgPortfolio
            portfolioList.Add(trans);
            dgPortfolio.Items.Refresh();

            //insert the data to Table "Transcation

        }
    }
}
