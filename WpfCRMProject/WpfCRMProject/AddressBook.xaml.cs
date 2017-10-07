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
        string firstName, lastName, company, street, city, province, postalCode, country, phone1, phone2, email, web;

        private void btnCompanyCancelDetail_Click(object sender, RoutedEventArgs e)
        {
            DisplayDetail();
        }

        private void btnCompanySaveDetail_Click(object sender, RoutedEventArgs e)
        {
            if( (tbFirstName.IsEnabled ==true) &&(
                firstName != tbFirstName.Text ||
                lastName != tbLastName.Text ||
                company != tbCompanyName.Text ||
                street != tbStreet.Text ||
                city != tbCity.Text ||
                province != tbProvince.Text ||
                postalCode != tbPostal.Text ||
                country != tbCountry.Text ||
                phone1 != tbPhone1.Text ||
                phone2 != tbPhone2.Text ||
                email != tbEmail.Text)
                )
            {
                Customer customer = new Customer
                {
                    ContactFirstName = tbFirstName.Text,
                    ContactLastName = tbLastName.Text,
                    CompanyName = tbCompanyName.Text,
                    Street = tbStreet.Text,
                    City = tbCity.Text,
                    Province = tbProvince.Text,
                    Postal = tbPostal.Text,
                    Country = tbCountry.Text,
                    Phone = tbPhone1.Text,
                    Fax = tbPhone2.Text,
                    Email = tbEmail.Text
                };
                
                db.UpdateCustomer(customer);
            }
            else
            {
                MessageBox.Show("test, nothing change");
            }
        }

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
            DisplayDetail();
        }

        public void DisplayDetail()
        {
            if (lvAddress.SelectedItem != null)
            {
                DisableEnableTextBox(false);
                Customer customerSelected = (Customer)lvAddress.SelectedItem;
                tbFirstName.Text = customerSelected.ContactFirstName;
                tbLastName.Text = customerSelected.ContactLastName;
                tbCompanyName.Text = customerSelected.CompanyName;
                tbStreet.Text = customerSelected.Street;
                tbCity.Text = customerSelected.City;
                tbProvince.Text = customerSelected.Province;
                tbPostal.Text = customerSelected.Postal;
                tbCountry.Text = customerSelected.Country;
                tbPhone1.Text = customerSelected.Phone;
                tbPhone2.Text = customerSelected.Fax;
                tbEmail.Text = customerSelected.Email;
                lblSalesRep.Content = customerSelected.SalesRepId;

            }
        }

        private void btnCompanyEditDetail_Click(object sender, RoutedEventArgs e)
        {
            if (lblSalesRep.Content.ToString() != Application.Current.Resources["UserName"].ToString())
            {
                btnCompanyEditDetail.IsEnabled = false;
                btnCompanySaveDetail.IsEnabled = false;
            }
            else
            {
                DisableEnableTextBox(true);
                firstName = tbFirstName.Text;
                lastName = tbLastName.Text;
                company = tbCompanyName.Text;
                city = tbCity.Text;
                street = tbStreet.Text;
                city = tbCity.Text;
                province = tbProvince.Text;
                postalCode = tbPostal.Text;
                country = tbCountry.Text;
                phone1 = tbPhone1.Text;
                phone2 = tbPhone2.Text;
                email = tbEmail.Text;
            }
        }

        private void DisableEnableTextBox(bool toggle)
        {
            tbCity.IsEnabled = toggle;
            tbProvince.IsEnabled = toggle;
            tbPostal.IsEnabled = toggle;
            tbCountry.IsEnabled = toggle;
            tbCompanyName.IsEnabled = toggle;
            tbFirstName.IsEnabled = toggle;
            tbLastName.IsEnabled = toggle;
            tbEmail.IsEnabled = toggle;
            tbPhone1.IsEnabled = toggle;
            tbPhone2.IsEnabled = toggle;
            tbStreet.IsEnabled = toggle;
        }
    }
}