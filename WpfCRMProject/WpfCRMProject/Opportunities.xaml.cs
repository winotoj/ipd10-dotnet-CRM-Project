﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Opportunities.xaml
    /// </summary>
    public partial class Opportunities : Window
    {
        public Opportunities()
        {
            InitializeComponent();
        }

        private void btnAddressBook_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        //to be fixed because cannot open main window
        void Opportunities_Closing(object sender, CancelEventArgs e)
        {

            
            App.Current.MainWindow.Show();
            this.Close();
        }
    }
}
