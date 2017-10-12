﻿using System;
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
using WpfCRMProject.Domain;


namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for AddSchedule.xaml
    /// </summary>
    public partial class AddSchedule : Window
    {
        Database db;
        public AddSchedule()
        {
            InitializeComponent();
        }

        private void tbSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new Database();               

                List<Customer> companyName = db.SearchCompanyName( tbCompanyName.Text );
                lvCompanyName.Items.Clear();
                foreach (Customer c in companyName)
                {
                    lvCompanyName.Items.Add(c);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

               
private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            //String type = ((ComboBoxItem)(cbType.SelectedValue)).ToString();

            ComboBoxItem typeItem = (ComboBoxItem)cbType.SelectedItem;
            String type = typeItem.Content.ToString();

            String subject = tbSubject.Text;
            String meetingDate = dateOfMetting.Text;
            //String startTime = cbStartTime.Text;
            //String endTime = cbEndTime.Text;
            String note = tbNote.Text;
            Customer customer = (Customer)lvCompanyName.SelectedItem;


            Schedule schedule = new Schedule();
            schedule.CustomerID = customer.CustomerId;
            schedule.Type = type;
            schedule.Subject = subject;
            schedule.ScheduleDate = DateTime.Parse(meetingDate);
            //schedule.StartTime = startTime;
            //schedule.EndTime = endTime;
            schedule.Note = note;
            schedule.SalesRepId = Convert.ToInt32(Application.Current.Resources["UserName"]);

            try
            {
                db = new Database();
                if (!db.CheckAppointment(schedule))
                {
                    db.AddAppointment(schedule);
                }
                else
                {
                    MessageBox.Show("You have an appointment at" + schedule.ScheduleDate + " between " + schedule.StartTime +
                                          " and " + schedule.EndTime + "\n Please select another date and time.", "Add Appointment", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Your Appointment successfully registered!!", "Add Appointment", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
