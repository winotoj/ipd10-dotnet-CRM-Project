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
    /// Interaction logic for Opportunity.xaml
    /// </summary>
    public partial class Opportunity : Page
    {
        Repors db;
        public Opportunity()
        {
            try
            {
                db = new Repors();
                InitializeComponent();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            DisplayOpportunities();
        }

        public void DisplayOpportunities()
        {
            List<Customer> listOpportunities = db.GetOpportunities();
            lvAddress.Items.Clear();
            foreach (Customer c in listOpportunities)
            {
                lvAddress.Items.Add(c);

            }
        }

        //to be fixed because cannot open main window
        //void Opportunities_Closing(object sender, CancelEventArgs e)
        //{

        //    e.Cancel = true;
        //    App.Current.MainWindow.Show();
        //    //this.Close();
        //}

        //private void btnAddnewCustomer_Click(object sender, RoutedEventArgs e)
        //{
        //    AddCustomer windowAdd = new AddCustomer();
        //    windowAdd.ShowDialog();

        //}
    }
}
