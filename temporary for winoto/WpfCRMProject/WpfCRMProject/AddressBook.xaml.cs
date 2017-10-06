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
            if(lvAddress.SelectedItem != null)
            {
                DisableEnableTextBox(false);
                Customer customerSelected = (Customer)lvAddress.SelectedItem;
                tbContactName.Text = customerSelected.ContactFirstName + " " + customerSelected.ContactLastName;
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
            if(lblSalesRep.Content.ToString() != Application.Current.Resources["UserName"].ToString())
            {


                MessageBox.Show(lblSalesRep.Content + "---" + Application.Current.Resources["UserName"]);
            }
            else
            {
                DisableEnableTextBox(true);

            }
        }

        private void DisableEnableTextBox(bool toggle)
        {
            tbCity.IsEnabled = toggle;
            tbProvince.IsEnabled = toggle;
            tbPostal.IsEnabled = toggle;
            tbCountry.IsEnabled = toggle;
            tbCompanyName.IsEnabled = toggle;
            tbContactName.IsEnabled = toggle;
            tbEmail.IsEnabled = toggle;
            tbPhone1.IsEnabled = toggle;
            tbPhone2.IsEnabled = toggle;
            tbStreet.IsEnabled = toggle;
        }
    }
}

 

//foreach (Control ctl in containerCanvas.Children)
//            {
//                if (ctl.GetType() == typeof(RadioButton))
//                    ((RadioButton) ctl).IsChecked = false;
//                if (ctl.GetType() == typeof(ComboBox))
//                    ((ComboBox) ctl).Text = String.Empty;
//                if (ctl.GetType() == typeof(CheckBox))
//                    ((CheckBox) ctl).IsChecked = false;
//                if (ctl.GetType() == typeof(TextBox))
//                    ((TextBox) ctl).Text = String.Empty;
//                if (ctl.GetType() == typeof(Slider))
//                    ((Slider) ctl).Value=0;
//            }