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

namespace FrameWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database db;
        public MainWindow()
        {
            try
            {
                db = new Database();
                MessageBox.Show("database is connected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {

                MessageBox.Show("Fatal error: unable to connect to database", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                // TODO: Write details of the exception to log text file
                throw e;
            }
            InitializeComponent();
            //Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }
    }
}
