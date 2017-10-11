using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private string _Sender;
        private string _Email;
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
        }
        public string GetEmail()
        {
            _Email = db.GetUserEmail(sender);
            return _Email;
        }

        public string recipient{
            get
            {
                return _Recipient;
            }
            set
            {
                _Recipient = value;
            }
        }
        public string sender
        {
            get
            {
                return _Sender;
            }
            set
            {
                _Sender = value;
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
                        db.RecordMessage(tbSubject.Text, tbBody.Text, "Email", _CompanyId, int.Parse(_Sender));
                    tbSubject.Text = string.Empty;
                    tbBody.Text = string.Empty;
                        this.Close();
                        
                        
                    }catch (System.Runtime.InteropServices.COMException ex)
                    {
                        MessageBox.Show("Error sending email" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                
                


            }
            
            
        }
        void SendEmail_Closing(object sender, CancelEventArgs e)
        {
            if (tbSubject.Text != string.Empty || tbBody.Text != string.Empty)
            {
                e.Cancel = true;
                MessageBoxResult result = MessageBox.Show("Are you sure want to cancel?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    e.Cancel = false;
                }

            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if(tbSubject.Text != string.Empty || tbBody.Text != string.Empty)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure want to cancel?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
    }
}
