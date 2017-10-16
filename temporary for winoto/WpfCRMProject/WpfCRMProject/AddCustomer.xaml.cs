﻿using System;
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

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            String companyName = tbCompanyName.Text;
            if (companyName.Length < 2)
                throw new ApplicationException(" must be greater than 2");
            String firstName = tbFirstName.Text;
            String lastName = tbLastName.Text;
            String phoneNum = tbPhoneNum.Text;
            String fax = tbFax.Text;
            String Email = tbEmail.Text;
            String webSite = tbWebsite.Text;
            String street = tbStreet.Text;
            String city = tbcity.Text;
            String postalCode = tbPostalCode.Text;
            String province = tbProvince.Text;
            String country = tbCountry.Text;
            Boolean status;
            if (rbCustomer.IsChecked == true)
            {
                status = true;
            }
            else status = false;
            BindingExpression be = tbCompanyName.GetBindingExpression(TextBox.TextProperty);
            if (be.HasValidationError)
            {
                tbCompanyName.BorderBrush = Brushes.Red;
            }

            Customer newCustomer = new Customer
            {

                CompanyName = companyName,
                Street = street,
                City = city,
                Province = province,
                Postal = postalCode,
                Phone = phoneNum,
                Fax = fax,
                ContactFirstName = firstName,
                ContactLastName = lastName,
                Country = country,
                CreateDate = DateTime.Today,
                Status = status,
                Email = Email

            };
            db.AddPerson(newCustomer);
      
            MessageBox.Show("New Customer is added", "Successfully message", MessageBoxButton.OK, MessageBoxImage.Information);
            var mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            //mainWin.btnOpportunity.Focus();
            //mainWin.btnAddressBook.Focus();

            this.Close();
            AddressBook addressBook = new AddressBook();
            //addressBook.DisplayAddressBook();

            List<Customer> listCustomer = db.GetAllCustomers();
            addressBook.lvAddress.Items.Clear();

            foreach (Customer c in listCustomer)
            {
                addressBook.lvAddress.Items.Add(c);

            }

            mainWin.frTest.Refresh();

            //MessageBox.Show("New Customer is added", "Successfully message", MessageBoxButton.OK, MessageBoxImage.Information);

            //AddressBook addressBook = new AddressBook();
            // addressBook.DisplayAddressBook();



            this.Close();
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
           
    
    
