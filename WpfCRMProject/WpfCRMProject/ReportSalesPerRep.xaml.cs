using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
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
    /// Interaction logic for ReportSalesPerRep.xaml
    /// </summary>
    public partial class ReportSalesPerRep : Window
    {
        Database db;
        public ReportSalesPerRep()
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
            
            LoadLineChartData();
        }

        private void LoadLineChartData()
        {
            List<SalesPerRep> list = db.GetSalesYTD();
            string str = "";
            int id = 0;
            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (id == list[i].RepId || id== 0)
                {
                    id = list[i].RepId;
                    ((LineSeries)mcChart.Series[count]).ItemsSource =
            new KeyValuePair<int, decimal>[]{
 new KeyValuePair<int,decimal>(list[i].Mth, list[i].Total)};
                   
                }
            }
             
 //           ((LineSeries)mcChart.Series[0]).ItemsSource =
 //           new KeyValuePair<DateTime, int>[]{
 //new KeyValuePair<DateTime,int>(DateTime.Now, 100),
 //new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(1), 130),
 //new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(2), 150),
 //new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(3), 125),
 //new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(4),155) };
        }
    }
}
