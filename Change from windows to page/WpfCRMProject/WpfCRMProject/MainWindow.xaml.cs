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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpportunities_Click(object sender, RoutedEventArgs e)
        {
            frTest.Navigate(new System.Uri("Opportunity.xaml",
UriKind.RelativeOrAbsolute));
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frTest.Navigate(new System.Uri("AddressBook.xaml",
UriKind.RelativeOrAbsolute));
        }
    }
}
