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
using WpfCRMProject.Domain;

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for SearchCompany.xaml
    /// </summary>
    public partial class SearchCompany : Window
    {
        Database db;
        public string str;
        
        public SearchCompany()

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
           
            List<User> listUser = db.GetAllUsers();
            cbSalesRep.Items.Clear();
            int count = 0;
            foreach (User c in listUser)
            {
                cbSalesRep.Items.Add(c);
                if (c.UserId.ToString() == Application.Current.Resources["UserName"].ToString())
                {
                    cbSalesRep.SelectedIndex = count;
                }
                count++;
                
            }
            dpLastPurchaseDate.SelectedDate = DateTime.Today;

        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string strquery = "";
            if (rbCustomer.IsChecked == true)
            {
                strquery += " c.status = 1";
            }
            else
            {
                strquery += " c.status = 0";
            }
            if (tbCompanyName.Text != "")
            {
                strquery += " and c.company_name like'%" + tbCompanyName.Text.Trim() + "%'";
            }
            if (tbFirstName.Text != "")
            {
                strquery += " and c.contact_firstName like'%" + tbFirstName.Text.Trim() + "%'";
            }
            if (tbLastName.Text != "")
            {
                strquery += " and c.contact_lastName like'%" + tbLastName.Text.Trim() + "%'";
            }
            if (tbPhoneNum.Text != "")
            {
                strquery += " and c.phone like'%" + tbPhoneNum.Text.Trim() + "%'";
            }
            if (tbFax.Text != "")
            {
                strquery += " and c.fax like'%" + tbFax.Text.Trim() + "%'";
            }
            if (tbEmail.Text != "")
            {
                strquery += " and c.email like'%" + tbEmail.Text.Trim() + "%'";
            }
            if (tbWebsite.Text != "")
            {
                strquery += " and c.web like'%" + tbWebsite.Text.Trim() + "%'";
            }
            if (tbStreet.Text != "")
            {
                strquery += " and c.street like'%" + tbStreet.Text.Trim() + "%'";
            }
            if (tbcity.Text != "")
            {
                strquery += " and c.city like'%" + tbcity.Text.Trim() + "%'";
            }
            if (tbProvince.Text != "")
            {
                strquery += " and c.province like'%" + tbProvince.Text.Trim() + "%'";
            }
            if (tbPostalCode.Text != "")
            {
                strquery += " and c.postal like'%" + tbPostalCode.Text.Trim() + "%'";
            }
            if (tbCountry.Text != "")
            {
                strquery += " and c.country like'%" + tbCountry.Text.Trim() + "%'";
            }
            if (dpLastPurchaseDate.SelectedDate != null)
            {
                strquery += " and v.purchase_date > " + dpLastPurchaseDate.SelectedDate.Value.ToShortDateString();
            }
           
            User cbUser = (User)cbSalesRep.SelectedItem;
            strquery += " and c.salesrep_Id = " + cbUser.UserId.ToString();
            str = strquery;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            str = "";
            this.Close();
        }
    }
}
        