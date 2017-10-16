using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfCRMProject.Domain;

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : Page
    {
        Database db;
        public Calendar()
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
            DisplayWorkDay();            
        }

        //Get all dates of Meetings from List
        public List<DateTime> showMeetingOnCalendar()
        { 
            List<DateTime> date = new List<DateTime>();
            for (int i = 0; i < lvShowWorkDay.Items.Count; i++)
            {
                Schedule datelist = (Schedule)lvShowWorkDay.Items[i];
                date.Add(datelist.ScheduleDate);                
            }
            return date;
        }

        //Make a New Appointment
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddSchedule schedule = new AddSchedule();
            schedule.ShowDialog();
        }

        //Show All appointment based on userID
        public void DisplayWorkDay()
        {
            List<Schedule> listSchedule = db.GetAllAppointment();
            lvShowWorkDay.Items.Clear();
            foreach (Schedule c in listSchedule)
            {
                lvShowWorkDay.Items.Add(c);

            }
           
        }

        //Edit Selected appointment
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {            
            Schedule scheduleList = (Schedule)lvShowWorkDay.SelectedItem;
            if (scheduleList != null)
            {
                var addSchedule = new EditAppointment();
                addSchedule.tbCustomerName.Text = scheduleList.CustomerName;
                addSchedule.tbSubject.Text = scheduleList.Subject;
                addSchedule.dpDate.Text = scheduleList.ScheduleDate.ToString();
                addSchedule.tpStart.Text = scheduleList.StartTime;
                addSchedule.tpEnd.Text = scheduleList.EndTime;
                addSchedule.lblScheduleID.Content = scheduleList.Schedule_id;
                addSchedule.cmbState.SelectedIndex = scheduleList.Status;
                addSchedule.lblCustomerID.Content = scheduleList.CustomerID;
                addSchedule.dpType.Text = scheduleList.Type;
                addSchedule.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select one customer", "Edit Customer", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }
    }
}
