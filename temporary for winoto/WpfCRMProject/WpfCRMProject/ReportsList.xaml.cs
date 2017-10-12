using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for ReportsList.xaml
    /// </summary>
    public partial class ReportsList : Page
    {
        public ReportsList()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Report1 report1 = new Report1();
            report1.ShowDialog();
        }
    }
}
