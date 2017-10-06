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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for AddressBook.xaml
    /// </summary>
    public partial class AddressBook : Page
    {
        Database db;
        public AddressBook()
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
            DisplayAddressBook();


        }
        public void DisplayAddressBook()
        {
            List<Customer> listCustomer = db.GetAllCustomers();
            lvAddress.Items.Clear();
            foreach (Customer c in listCustomer)
            {
                lvAddress.Items.Add(c);

            }
        }
        private void lvAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvAddress.SelectedItem != null)
            {
                Customer customerSelected = (Customer)lvAddress.SelectedItem;
                lblContactName.Content = customerSelected.ContactFirstName + " " + customerSelected.ContactLastName;
                lblCompanyName.Content = customerSelected.CompanyName;
                lblStreet.Content = customerSelected.Street;
                lblAddress.Content = customerSelected.City + "\n" + customerSelected.Province + ", " + customerSelected.Postal + " " + customerSelected.Country;
                lblPhone1.Content = customerSelected.Phone;
                lblPhone2.Content = customerSelected.Fax;
                lblEmail.Content = customerSelected.Email;

            }
        }

        private void btnCompanyEditDetail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}