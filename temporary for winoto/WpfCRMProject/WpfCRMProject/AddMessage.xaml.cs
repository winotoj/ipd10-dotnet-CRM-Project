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
    /// Interaction logic for AddMessage.xaml
    /// </summary>
    public partial class AddMessage : Window
    {
        Database db;
        int customerId, salesRep;
        public AddMessage(int customerId, int salesRep)
        {
            try
            {
                db = new Database();
                InitializeComponent();
                this.customerId = customerId;
                this.salesRep = salesRep;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }        
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            string subject = txtSubject.Text;
            string note = txtNote.Text;
            string type = cbType.SelectedValue.ToString();
            db.AddMessage(customerId, subject, note, type, salesRep);
            this.Close();
        }
    }
}
