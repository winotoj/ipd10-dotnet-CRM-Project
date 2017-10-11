using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfCRMProject.Domain;

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for AddressBook.xaml
    /// </summary>
    public partial class AddressBook : Page
    {

   
        public static string lastHeaderAddress = string.Empty;

        Database db;

        string firstName, lastName, company, street, city, province, postalCode, country, phone1, phone2, email, web;

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

        private void btnAddMessage_Click(object sender, RoutedEventArgs e)
        {
            Customer customerSelected = (Customer)lvAddress.SelectedItem;
            AddMessage addMessage = new AddMessage((int)customerSelected.CustomerId, (int)customerSelected.SalesRepId);
            addMessage.ShowDialog();
            DisplayHistory();
        }

        private void tbEmail_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (tbEmail.Text != "")
            {
                Customer customerSelected = (Customer)lvAddress.SelectedItem;
                SendEmail sendEmail = new SendEmail();
                sendEmail.recipient = customerSelected.Email;
                sendEmail.companyId = (int)customerSelected.CustomerId;
                sendEmail.sender = customerSelected.SalesRepId.ToString();
                string email = sendEmail.GetEmail();
                sendEmail.tbTo.Text = customerSelected.Email;
                sendEmail.tbFrom.Text = email;
                sendEmail.ShowDialog();
            }
        }

        private void btnCompanySaveDetail_Click(object sender, RoutedEventArgs e)
        {
            if ((tbFirstName.IsEnabled == true) && (
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
                try
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
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Error inputing data" + ex.Message, "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            else
            {
                MessageBox.Show("test, nothing change");
            }
        }

        
        public void ClearItem()
        {
            lvAddress.Items.Clear();
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
            DisplayHistory();
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
                lblCustomerId.Content = customerSelected.CustomerId;

            }
        }

        public void DisplayHistory()
        {
            lvHistory.Items.Clear();
            if (lvAddress.SelectedItem != null)
            {
                Customer customerSelected = (Customer)lvAddress.SelectedItem;
                List<Messages> messages = db.GetMessages((int)customerSelected.CustomerId);
                foreach (Messages m in messages)
                {
                    lvHistory.Items.Add(m);
                }
                
            }
        }

        private void btnCompanyEditDetail_Click(object sender, RoutedEventArgs e)
        {
            if (lblSalesRep.Content.ToString() != Application.Current.Resources["UserName"].ToString() || Application.Current.Resources["role"].ToString() != "manager")
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
            tbEmail.IsReadOnly = toggle;
            tbPhone1.IsEnabled = toggle;
            tbPhone2.IsEnabled = toggle;
            tbStreet.IsEnabled = toggle;
        }
        
        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            string header = ((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString();
            List<Customer> list = db.GetAllCustomers();
            if (lastHeaderAddress == header)
            {
                switch (header)
                {
                    case "Customer Name":
                        list.Sort((x, y) => -1 * x.CompanyName.CompareTo(y.CompanyName));
                        break;
                    case "Cust No":
                        list.Sort((x, y) => -1 * x.CustomerId.CompareTo(y.CustomerId));
                        break;
                    case "Status":
                        list.Sort((x, y) => -1 * x.Status.CompareTo(y.Status));
                        break;
                    case "Created on":
                        list.Sort((x, y) => -1 * x.CreateDate.CompareTo(y.CreateDate));
                        break;
                    case "Last Purch Date":
                        list.Sort((x, y) => -1 * x.LastPurchaseDate.CompareTo(y.LastPurchaseDate));
                        break;
                    case "Last Purch Amount":
                        list.Sort((x, y) => -1 * x.Amount.CompareTo(y.Amount));
                        break;
                    default:
                        return;
                }
                lastHeaderAddress = string.Empty;
            }
            else
            {
                switch (header)
                {
                    case "Customer Name":
                        list.Sort((x, y) => x.CompanyName.CompareTo(y.CompanyName));
                        break;
                    case "Cust No":
                        list.Sort((x, y) => x.CustomerId.CompareTo(y.CustomerId));
                        break;
                    case "Status":
                        list.Sort((x, y) => x.Status.CompareTo(y.Status));
                        break;
                    case "Created on":
                        list.Sort((x, y) => x.CreateDate.CompareTo(y.CreateDate));
                        break;
                    case "Last Purch Date":
                        list.Sort((x, y) => x.LastPurchaseDate.CompareTo(y.LastPurchaseDate));
                        break;
                    case "Last Purch Amount":
                        list.Sort((x, y) => x.Amount.CompareTo(y.Amount));
                        break;
                    default:
                        return;
                }
                lastHeaderAddress = header;
            }

                lvAddress.Items.Clear();
                foreach (Customer c in list)
                {
                    lvAddress.Items.Add(c);
                }

            }
      
    }
}
