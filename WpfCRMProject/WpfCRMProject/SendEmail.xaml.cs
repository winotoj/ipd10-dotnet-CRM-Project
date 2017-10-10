using System;
using System.Collections.Generic;
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
        public SendEmail()
        {
            InitializeComponent();
            tbFrom.Text = Application.Current.Resources["UserName"].ToString();
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
                {
                    //try
                    //{
                    //    Outlook._Application _app = new Outlook.Application();
                    //    Outlook.MailItem mail = (Outlook.MailItem)_app.CreateItem(Outlook.OlItemType.olMailItem);
                    //    mail.To = _Recipient;
                    //    mail.Subject = tbSubject.Text;
                    //    mail.Body = tbBody.Text;
                    //    mail.Importance = Outlook.OlImportance.olImportanceNormal;
                    //    ((Outlook._MailItem)mail).Send();
                    //    MessageBox.Show("Your message has been successfully sent", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    //    this.Close();
                    //}
                    //catch (System.Runtime.InteropServices.COMException ex)
                    //{
                    //    MessageBox.Show("fail " + ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    //}

                    //NEED TO FIX THE TRY CATCH AND NEED TO ADD TIME
                    Outlook._Application _app;
                    try
                    {
                        //outook has to be opened by admin
                        _app = (Outlook.Application)Marshal.GetActiveObject("Outlook.Application");
                        Outlook.MailItem mail = (Outlook.MailItem)_app.CreateItem(Outlook.OlItemType.olMailItem);
                        mail.To = _Recipient;
                        mail.Subject = tbSubject.Text;
                        mail.Body = tbBody.Text;
                        mail.Importance = Outlook.OlImportance.olImportanceNormal;
                        ((Outlook._MailItem)mail).Send();
                        MessageBox.Show("Your message has been successfully sent", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();

                    }
                    catch (System.Runtime.InteropServices.COMException ex)
                    {
                        MessageBox.Show("Outlook is not opened, click OK to try again" + ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
         
                            _app = new Outlook.Application();
                            Outlook.MailItem mail = (Outlook.MailItem)_app.CreateItem(Outlook.OlItemType.olMailItem);
                            mail.To = _Recipient;
                            mail.Subject = tbSubject.Text;
                            mail.Body = tbBody.Text;
                            mail.Importance = Outlook.OlImportance.olImportanceNormal;
                            ((Outlook._MailItem)mail).Send();
                            MessageBox.Show("Your message has been successfully sent", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        
                    }


                }


            }
            
            
        }
    }
}
