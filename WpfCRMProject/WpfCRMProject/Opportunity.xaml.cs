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
using WpfCRMProject.Domain;

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for Opportunity.xaml
    /// </summary>
    public partial class Opportunity : Page
    {
        Database db;
        string firstName, lastName, company, street, city, province, postalCode, country, phone1, phone2, email, web, strquery;
        bool search = false;
        public Opportunity()
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
            DisplayOpportunities();
        }

        //For serach Result
        public Opportunity(string s)
        {
            try
            {
                db = new Database();
                InitializeComponent();
                strquery = s;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            DisplayOpportunities(strquery);
        }

        private void lvAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayDetail();
            DisplayHistory();
            
        }

        public void DisplayOpportunities()
        {
            List<Customer> listOpportunities = db.GetOpportunities();
            search = false;
            lvAddress.Items.Clear();
            foreach (Customer c in listOpportunities)
            {
                lvAddress.Items.Add(c);

            }
        }

        List<Customer> list;
        string qry;

        public void DisplayOpportunities(string s)
        {
            qry = s;
            list = db.SearchCompanyCustom(qry);
            foreach (Customer l in list)
            {
                if (string.IsNullOrEmpty(l.Amount))
                {
                    l.Amount = "0";
                    l.LastPurchaseDate = "1900/12/31";
                }
            }
            //lblTitle.Content = "Search Result: " + list.Count + " Record(s)";
            search = true;
            lvAddress.Items.Clear();
            foreach (Customer c in list)
            {
                lvAddress.Items.Add(c);
            }
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
                lblSalesRep.Content = customerSelected.SalesRepId.ToString();
                lblCustomerId.Content = customerSelected.CustomerId;

            }
        }

        private void btnCompanyEditDetail_Click(object sender, RoutedEventArgs e)
        {           
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
            if (Application.Current.Resources["Role"].ToString() == "manager" || lblSalesRep.Content.ToString() == Application.Current.Resources["UserName"].ToString())
            {
                btnCompanyEditDetail.IsEnabled = true;
                btnCompanySaveDetail.IsEnabled = true;
                DisableEnableTextBox(true);
            }
            else
            {
                btnCompanyEditDetail.IsEnabled = false;
                btnCompanySaveDetail.IsEnabled = false;
                DisableEnableTextBox(false);
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
                        Email = tbEmail.Text,
                        CustomerId = Convert.ToInt32(lblCustomerId.Content)

                    };

                    db.UpdateCustomer(customer);
                    MessageBox.Show("Information successfully updated.");
                    DisableEnableTextBox(false);
                    if (!search)
                    {
                        DisplayOpportunities();
                        DisplayDetail();
                        DisplayHistory();                        
                    }
                    else
                    {
                        DisplayOpportunities();

                    }

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

    }
}
