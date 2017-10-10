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
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Reflection;
using Microsoft.Win32;

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
            var reducedList = customerList.Select(c => new { CompanyName = c.CompanyName , NumberOfBuys = c.SalesRepId }).ToList();
            

            var datatable = CreateExcelFile.ListToDataTable(reducedList);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel file (*.xls)|*.xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                CreateExcelFile.CreateExcelDocument(datatable, saveFileDialog.FileName);
                MessageBox.Show("All Information exported!", "Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnPrinttoPDF_Click(object sender, RoutedEventArgs e)
        {
            List<Customer> customerList = rp.DisplayListOfPurchesOfCustomers();
            lvAddress.Items.Clear();
            foreach (Customer c in customerList)
            {
                lvAddress.Items.Add(c);

            }
            var reducedList = customerList.Select(c => new { CompanyName = c.CompanyName, NumberOfBuys = c.SalesRepId }).ToList();

            DataTable dataTable = new DataTable(typeof(Customer).Name);
            var datatable = ToDataTable(reducedList);
            ExportToPdf(datatable);            
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }



        public void ExportToPdf(DataTable dt)
        {
            Document document = new Document();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                
                document.Open();

                iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

                PdfPTable table = new PdfPTable(dt.Columns.Count);

                float[] widths = new float[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    widths[i] = 4f;
                }

                table.SetWidths(widths);

                table.WidthPercentage = 100;


                foreach (DataColumn c in dt.Columns)
                {
                    table.AddCell(new Phrase(c.ColumnName, font5));
                }

                foreach (DataRow r in dt.Rows)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            table.AddCell(new Phrase(r[i].ToString(), font5));
                        }
                    }
                }
                document.Add(table);
                document.Close();
            }
        }
    }
}
