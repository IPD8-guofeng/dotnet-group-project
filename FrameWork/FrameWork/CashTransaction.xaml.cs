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
    /// Interaction logic for CashTransaction.xaml
    /// </summary>
    public partial class CashTransaction : Window
    {
        public PortTransaction trans = new PortTransaction();
        public CashTransaction(string message)
        {
            InitializeComponent();
            lblMessage.Content = message;
        }
   

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            //check if the date is correct
            DateTime? date = dpkDate.SelectedDate;
            if (date == null)
            {
              MessageBox.Show("Choose a Date!", "Input Error",
              MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            trans.Date =(DateTime) date;

            //check the amount is correct
            double amt;
            if(!double.TryParse(tbAmount.Text, out amt))
                {
              MessageBox.Show("please input a number! " + tbAmount.Text, "Input Error",
              MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            trans.Cashvalue = amt;
            trans.Notes = tbNotes.Text;

            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            dpkDate.Focus();
        }
    }
}
