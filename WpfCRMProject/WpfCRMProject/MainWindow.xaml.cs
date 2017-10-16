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
using System.Windows.Threading;

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
            frTest.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
            //Login logs = new Login();
            lbluserlogin.Content = Application.Current.Resources["FirstName"] + " " + Application.Current.Resources["LastName"];
            lblDate.Content = DateTime.Now.ToLongDateString();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            btnAddNewCustomer.Visibility = Visibility.Hidden;
            btnSearch.Visibility = Visibility.Hidden;
            Calendar calendar = new Calendar();
            frTest.Navigate(new System.Uri("Calendar.xaml", UriKind.RelativeOrAbsolute));

        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to exit?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
                Environment.Exit(0);
        }

        private void btnOpportunity_Click(object sender, RoutedEventArgs e)
        {
            btnAddNewCustomer.Visibility = Visibility.Visible;
            btnSearch.Visibility = Visibility.Visible;
            frTest.Navigate(new System.Uri("Opportunity.xaml",
UriKind.RelativeOrAbsolute));

        }

        private void btnAddressBook_Click(object sender, RoutedEventArgs e)
        {
            btnAddNewCustomer.Visibility = Visibility.Visible;
            btnSearch.Visibility = Visibility.Visible;
            AddressBook addressBook = new AddressBook();
            addressBook.DisplayAddressBook();
            frTest.NavigationService.Navigate(addressBook);
        }

        private void btnCalendar_Click(object sender, RoutedEventArgs e)
        {
            btnAddNewCustomer.Visibility = Visibility.Hidden;
            btnSearch.Visibility = Visibility.Hidden;
            frTest.Navigate(new System.Uri("Calendar.xaml",
UriKind.RelativeOrAbsolute));
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            btnAddNewCustomer.Visibility = Visibility.Hidden;
            btnSearch.Visibility = Visibility.Hidden;
            frTest.Navigate(new System.Uri("ReportsList.xaml",
UriKind.RelativeOrAbsolute));
        }

        private void btnAddNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer windowAdd = new AddCustomer();
            windowAdd.ShowDialog();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchCompany searchCompany = new SearchCompany();
            searchCompany.ShowDialog();
            string str = searchCompany.str;
            if (string.IsNullOrEmpty(str))
            {
                str = " c.salesrep_Id = " + Application.Current.Resources["UserName"].ToString();
            }
            frTest.NavigationService.Navigate(new AddressBook(str));

        }
    }
}