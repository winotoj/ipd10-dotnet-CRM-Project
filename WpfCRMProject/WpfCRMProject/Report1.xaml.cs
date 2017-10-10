using ExportToExcel;
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
using WpfCRMProject.Services;

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for Report1.xaml
    /// </summary>
    public partial class Report1 : Window
    {
        Reports rp;
        public Report1()
        {
            
            try
            {
                rp = new Reports();
                InitializeComponent();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            DisplayListOfPurchesOfCustomers();
        }

        public void DisplayListOfPurchesOfCustomers()
        {
            List<Customer> customerList = rp.DisplayListOfPurchesOfCustomers();
            lvAddress.Items.Clear();
            foreach (Customer c in customerList)
            {
                lvAddress.Items.Add(c);

            }
        }


        private void btnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            List<Customer> customerList = rp.DisplayListOfPurchesOfCustomers();
            lvAddress.Items.Clear();
            foreach (Customer c in customerList)
            {
                lvAddress.Items.Add(c);

            }
            var reducedList = customerList.Select(c => new { c.CompanyName, c.SalesRepId }).ToList();
            

            var datatable = CreateExcelFile.ListToDataTable(reducedList);
            CreateExcelFile.CreateExcelDocument(datatable, "ListOfPurchesOfCustomers.xls");
            MessageBox.Show("All Information exported!", "Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
