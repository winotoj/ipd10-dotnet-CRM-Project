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
            list.Sort((a, b) => string.Compare(a.RepId.ToString(), b.RepId.ToString()));
            string FullName = "";
            int id = 0;
            int count = 0;
           

            foreach (SalesPerRep s in list)
            {
                if (id != s.RepId)
                {
                    id = s.RepId;
                    List<SalesPerRep> result = (from l in list where l.RepId == id select l).ToList();
                    Dictionary<int, decimal> salesrep = new Dictionary<int, decimal>();

                    foreach (SalesPerRep r in result)
                    {
                        salesrep.Add(r.Mth, r.Total);
                    }

                    ((LineSeries)mcChart.Series[count]).ItemsSource = salesrep;
                    FullName = s.FirstName + " " + s.LastName;
                    ((LineSeries)mcChart.Series[count]).Title = FullName;
                    count++;
                }
            }


        }
    }
    
}
