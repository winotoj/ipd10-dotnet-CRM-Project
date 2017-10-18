using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using WpfCRMProject.Services;
using WpfCRMProject.Domain;

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public Login()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);          
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        
        void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System && e.SystemKey == Key.F4)
            {
                e.Handled = true;
            }
        }
        
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbUserName.Text.Length == 0)
            {
                errormessage.Text = "Pleasse Enter your Username.";
                tbUserName.Focus();
            }

            else
            {
                string username = tbUserName.Text;
                string password = tbPassword.Password;
                User currentuser = null;

                try
                {
                    LoginService loginService = new LoginService();
                    currentuser = loginService.Login(username, password);
                }
                catch (SqlException)

                {
                    errormessage.Text = "Please enter correct username and Password";
                    
                   return;
                }

                if (currentuser != null)
                {                   
                    Application.Current.Resources.Add("UserName", username);
                    Application.Current.Resources.Add("FirstName", currentuser.FirstName);
                    Application.Current.Resources.Add("LastName", currentuser.LastName);
                    Application.Current.Resources.Add("Role", currentuser.Role);
                    Application.Current.Resources.Add("Email", currentuser.Email);
                    this.Close();
                }
                else
                {
                    errormessage.Text = "Sorry! Please enter existing Username And Password.";
                }

            }
        }
    }
}
