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

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for AddSchedule.xaml
    /// </summary>
    public partial class AddSchedule : Window
    {
        public AddSchedule()
        {
            InitializeComponent();
        }

        private void tbSearch_Click(object sender, RoutedEventArgs e)
        {
            Repors db;
            try
            {
                db = new Repors();
                db.SearchCompanyName("'%" + tbCompanyName.Text + "%'");
                //todo display result
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
