using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
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
using Outlook = Microsoft.Office.Interop.Outlook;

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for SendEmail.xaml
    /// </summary>
    public partial class SendEmail : Window
    {
        private string _Recipient;
        private int _CompanyId;
       Database db;
        public SendEmail()
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
            User user = new User();
            tbFrom.Text = user.Email.ToString();
           // tbTo.Text = _Recipient;
        }
        public string recipient{
            get
            {
                return _Recipient;
            }
            set
            {
                _Recipient = value;
                tbTo.Text = _Recipient;
            }
        }

        public int companyId
        {
            get
            {
                return _CompanyId;
            }
            set
            {
                _CompanyId = value;
            }
        }
        
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            
            if(tbTo.Text =="" || tbSubject.Text == "" || tbBody.Text =="")
            {
                MessageBox.Show("Please make sure recipient and subject is not empty","message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
            
                   
                    Outlook._Application _app;
                   

                    try
                    {
                        _app = (Outlook.Application)Marshal.GetActiveObject("Outlook.Application");
                    }
                   // catch (System.Runtime.InteropServices.COMException ex)
                   catch(Exception)
                    {
                     //MessageBox.Show("Outlook is not opened under Administrator, close OUtlook and click OK." + ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        _app = new Outlook.Application();
                    }
                    try
                    {
                        Outlook.MailItem mail = (Outlook.MailItem)_app.CreateItem(Outlook.OlItemType.olMailItem);
                        mail.To = _Recipient;
                        mail.Subject = tbSubject.Text;
                        mail.Body = tbBody.Text;
                        mail.Importance = Outlook.OlImportance.olImportanceNormal;
                        ((Outlook._MailItem)mail).Send();

                        MessageBox.Show("Your message has been successfully sent", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        db.RecordMessage(tbSubject.Text, tbBody.Text, "Email", _CompanyId);
                        this.Close();
                        
                        
                    }catch (System.Runtime.InteropServices.COMException ex)
                    {
                        MessageBox.Show("Error sending email" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                
                


            }
            
            
        }
       
    }
}
