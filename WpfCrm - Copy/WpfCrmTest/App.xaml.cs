using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCrmTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow myMainWindow = new MainWindow();
            Login myDialogWindow = new Login();
            //if user doesnt press cancel != null
            myDialogWindow.ShowDialog();
            if (Login.OpenApp)
            {
                myMainWindow.Show();
            } 
        }
    }
}
