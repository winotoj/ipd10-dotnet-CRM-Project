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
        //SELECT * fROM (salesreps as s left join customers as c on s.username = c.salesrep_Id) LEFT JOIN v_Sales_LatestPurchase as v on c.customer_id = v.customer_id WHERE c.salesrep_id = @salesrep_id and c.status=1
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string strquery = "";
            int counter = 0;
            if (rbCustomer.IsChecked == true)
            {
                counter++;
                strquery += " c.status = 1";
            }
            else
            {
                counter++;
                strquery += " c.status = 0";
            }
            if (tbCompanyName.Text != "")
            {
                counter++;
                strquery += "and c.company_name like'%" + tbCompanyName.Text.Trim() + "%'";
            }
            if (tbFirstName.Text != "")
            {
                counter++;
                strquery += "and c.contact_firstName like'%" + tbFirstName.Text.Trim() + "%'";
            }
            if (tbLastName.Text != "")
            {
                counter++;
                strquery += " and c.contact_lastName like'%" + tbLastName.Text.Trim() + "%'";
            }
            if (tbPhoneNum.Text != "")
            {
                counter++;
                strquery += " and c.phone like'%" + tbPhoneNum.Text.Trim() + "%'";
            }
            if (tbFax.Text != "")
            {
                counter++;
                strquery += " and c.fax like'%" + tbFax.Text.Trim() + "%'";
            }
            if (tbEmail.Text != "")
            {
                counter++;
                strquery += " and c.email like'%" + tbEmail.Text.Trim() + "%'";
            }
            if (tbWebsite.Text != "")
            {
                counter++;
                strquery += " and c.web like'%" + tbWebsite.Text.Trim() + "%'";
            }
            if (tbStreet.Text != "")
            {
                counter++;
                strquery += " and c.street like'%" + tbStreet.Text.Trim() + "%'";
            }
            if (tbcity.Text != "")
            {
                counter++;
                strquery += " and c.city like'%" + tbcity.Text.Trim() + "%'";
            }
            if (tbProvince.Text != "")
            {
                counter++;
                strquery += " and c.province like'%" + tbProvince.Text.Trim() + "%'";
            }
            if (tbPostalCode.Text != "")
            {
                counter++;
                strquery += " and c.postal like'%" + tbPostalCode.Text.Trim() + "%'";
            }
            if (tbCountry.Text != "")
            {
                counter++;
                strquery += " and c.country like'%" + tbCountry.Text.Trim() + "%'";
            }
            if (rbCustomer.IsChecked == true)
            {
                counter++;
                strquery += " and c.status = 1";
            }
            string inMyString;
           
                inMyString = dpLastPurchaseDate.SelectedDate.Value.ToShortDateString();
           

           // var inMyString = dpLastPurchaseDate.SelectedDate.Value.ToShortDateString();
            
            counter++;
            
           
            strquery += " and c.salesrep_Id = " + ((ComboBox)cbSalesRep.SelectedValue).ToString();
            MessageBox.Show("coounter is " + counter + "/n" + strquery + " " + inMyString);
        }
    }
}
        