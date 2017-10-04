using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to exit?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Login login = new Login();
                login.Close();
            }
                
        }
        
            
        private void btnOpportunity_Click(object sender, RoutedEventArgs e)
        {
            frTest.Navigate(new System.Uri("Opportunity.xaml",
UriKind.RelativeOrAbsolute));

        }

        private void btnAddressBook_Click(object sender, RoutedEventArgs e)
        {
            frTest.Navigate(new System.Uri("AddressBook.xaml",
UriKind.RelativeOrAbsolute));
        }
    }
}

