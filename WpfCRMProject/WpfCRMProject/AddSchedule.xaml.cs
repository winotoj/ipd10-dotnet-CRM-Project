using System;
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
        public AddSchedule()
        {
            InitializeComponent();
        }

        private void tbSearch_Click(object sender, RoutedEventArgs e)
        {
            Database db;
            try
            {
                db = new Database();               

                List<Customer> companyName = db.SearchCompanyName( tbCompanyName.Text );
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

               
private void Button_Click(object sender, RoutedEventArgs e)
        {
            String type = cbType.SelectedValue.ToString();
            String subject = tbSubject.Text;
            String meetingDate = dateOfMetting.Text;
            String startTime = cbstartTime.SelectedValue.ToString();
            String endTime = cbEndTime.SelectedValue.ToString();
            String note = tbNote.Text;

            Schedule schedule = new Schedule();
            schedule.Type = type;
            schedule.Subject = subject;
            schedule.ScheduleDate = DateTime.Parse(meetingDate);
            schedule.StartTime = startTime;
            schedule.EndTime = endTime;
            schedule.Note = note;

        }
    }
}
