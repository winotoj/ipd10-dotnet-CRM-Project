using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        Database db;
        private int _errors = 0;
        private Customer _Customer = new Customer();
        public AddCustomer()
        {
            try
            {
                db = new Database();
                InitializeComponent();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            gridCustomer.DataContext = _Customer;
        }
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _errors == 0;
            e.Handled = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {


            e.Handled = true;
            Boolean status;
            if (rbCustomer.IsChecked == true)
            {
                status = true;
            }
            else status = false;
            _Customer.Status = status;
            _Customer.CreateDate = DateTime.Now;

            db.AddPerson(_Customer);
            MessageBox.Show("New Customer is added", "Successfully message", MessageBoxButton.OK, MessageBoxImage.Information);
            var mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            //mainWin.btnOpportunity.Focus();
            //mainWin.btnAddressBook.Focus();

            this.Close();
            AddressBook addressBook = new AddressBook();

            List<Customer> listCustomer = db.GetAllCustomers();
            addressBook.lvAddress.Items.Clear();

            foreach (Customer c in listCustomer)
            {
                addressBook.lvAddress.Items.Add(c);

            }

            mainWin.frTest.Refresh();

        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _errors++;
            else
                _errors--;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            if ((result = MessageBox.Show("Are you sure want to cancel ?", "message", MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}



