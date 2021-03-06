﻿using System;
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
    /// Interaction logic for StockTransaction.xaml
    /// </summary>
    public partial class StockTransaction : Window
    {
        public PortTransaction trans = new PortTransaction();

        public StockTransaction(string message)
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
            //check if the Type is correct
            ComboBoxItem cmbItem = (ComboBoxItem)cmbType.SelectedItem;
            string type = cmbItem.Content.ToString();

            if (type == "")
            {
                MessageBox.Show("Choose a Type!", "Input Error",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (type == "Buy") { trans.Type = TransType.Buy; }
            else { trans.Type = TransType.Sell; }

            //check if the date is correct
            DateTime? date = dpkDate.SelectedDate;
            if (date == null)
            {
                MessageBox.Show("Choose a Date!", "Input Error",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            trans.Date = (DateTime)date;

            //check if the share amount is correct
            int share;
            if (!int.TryParse(tbShare.Text, out share))
            {
                MessageBox.Show("please input an integer number! ", "Input Error",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            trans.Share = share;

            //check if the price is correct
            double amt;
            if (!double.TryParse(tbAmount.Text, out amt))
            {
                MessageBox.Show("please input a number! ", "Input Error",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            trans.Price = amt;
            if (type == "Buy") { trans.Cashvalue = (-1) * share * amt; }
            else { trans.Cashvalue = share * amt; }
            trans.Notes = tbNotes.Text;

            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            dpkDate.Focus();
        }
    }
}
