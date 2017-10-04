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

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static bool OpenApp = false;
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
            System.Windows.Application.Current.Shutdown();
            System.Windows.Application.Current.MainWindow.Close();
        }
        void Login_Closing(object sender, CancelEventArgs e)
        {
            //needed otherwise when click OK will not open main window
            if (!OpenApp)
            {
                e.Cancel = true;

            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //to do checking user name before close this window and open main
            if (tbUserName.Text.Length == 0)
            {
                errormessage.Text = "Pleasse Enter your Username.";
                tbUserName.Focus();
            }
            
            else
            {
                string username = tbUserName.Text;
                string password = tbPassword.Password;
                SqlConnection con = new SqlConnection("Server=tcp:vwdotnetproject.database.windows.net,1433;Initial Catalog=CrmProject;Persist Security Info=False;User ID=vajiwinoto;Password=VW@azure;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM SalesReps WHERE username = '" + username + "'  AND PASSWORD = '" + password + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    MainWindow myworkday = new MainWindow();
                    string welcome = dataSet.Tables[0].Rows[0]["FirstName"].ToString() + " " + dataSet.Tables[0].Rows[0]["LastName"].ToString();
                    myworkday.lbluserlogin.Content = welcome;//Sending value from one form to another form.
                    this.Hide();
                    myworkday.Show();
                    Application.Current.Resources.Add("UserName", username);
                    Application.Current.Resources.Add("FirstName", dataSet.Tables[0].Rows[0]["FirstName"].ToString());
                    Application.Current.Resources.Add("LastName", dataSet.Tables[0].Rows[0]["LastName"].ToString());

                }
                else
                {
                    errormessage.Text = "Sorry! Please enter existing Username And Password.";
                }
                con.Close();
            }
        }
    }
}
