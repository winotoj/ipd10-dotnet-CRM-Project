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
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Runtime.InteropServices;

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for AddSchedule.xaml
    /// </summary>
    public partial class AddSchedule : Window
    {
        Database db;
        private int _errors = 0;
        private Schedule _Schedule = new Schedule();
        public AddSchedule()
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
            gridSchedule.DataContext = _Schedule;
        }

        private void tbSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Customer> companyName = db.SearchCompanyName(tbCompanyName.Text);
            lvCompanyName.Items.Clear();
            foreach (Customer c in companyName)
            {
                lvCompanyName.Items.Add(c);
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _errors == 0;
            e.Handled = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            Boolean status;
            if (lvCompanyName.SelectedItem == null)
            {
                MessageBox.Show("Please enter Company name or use Outlook for your personal schedule");
                return;
            }

            ComboBoxItem typeItem = (ComboBoxItem)cbType.SelectedItem;

            String type = "";
            if (cbType.SelectedIndex != -1)
            {
                type = typeItem.Content.ToString();
            }
            String meetingDate = dateOfMetting.Text;
            String startTime = cbStartTime.Text;
            String endTime = cbEndTime.Text;
            Customer customer = (Customer)lvCompanyName.SelectedItem;

           // Schedule schedule = new Schedule();
            _Schedule.CustomerID = customer.CustomerId;
            _Schedule.Type = type;
            _Schedule.ScheduleDate = DateTime.Parse(meetingDate);
            _Schedule.StartTime = startTime;
            _Schedule.EndTime = endTime;
            _Schedule.SalesRepId = Convert.ToInt32(Application.Current.Resources["UserName"]);

            Outlook._Application _app;
            try
            {
                _app = (Outlook.Application)Marshal.GetActiveObject("Outlook.Application");
            }
            // catch (System.Runtime.InteropServices.COMException ex)
            catch (Exception)
            {
                //MessageBox.Show("Outlook is not opened under Administrator, close OUtlook and click OK." + ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                _app = new Outlook.Application();
            }
            try
            {
                Outlook.AppointmentItem oAppointment = (Outlook.AppointmentItem)_app.CreateItem(Outlook.OlItemType.olAppointmentItem);
                oAppointment.Subject = _Schedule.Subject;
                oAppointment.Body = _Schedule.Note;
                oAppointment.Start = Convert.ToDateTime(meetingDate + " " + startTime);
                oAppointment.End = Convert.ToDateTime(meetingDate + " " + endTime);
                oAppointment.Importance = Outlook.OlImportance.olImportanceHigh;
                oAppointment.ReminderSet = true;
                oAppointment.ReminderMinutesBeforeStart = 30;
                oAppointment.BusyStatus = Outlook.OlBusyStatus.olBusy;
                oAppointment.Save();
                if (type == "Face To Face")
                {
                    Outlook.MailItem mailItem = oAppointment.ForwardAsVcal();
                    mailItem.To = customer.Email;
                    mailItem.Send();
                }



            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("Error sending email" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                db = new Database();
                if (!db.CheckAppointment(_Schedule))
                {
                    db.AddAppointment(_Schedule);
                }
                else
                {
                    MessageBox.Show("You have an appointment at" + _Schedule.ScheduleDate + " between " + _Schedule.StartTime +
                                          " and " + _Schedule.EndTime + "\n Please select another date and time.", "Add Appointment", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            //refresh the list
            var calendar = new Calendar();
            List<Schedule> scheduleList = db.GetAllAppointment();
            calendar.lvShowWorkDay.Items.Clear();
            foreach (Schedule s in scheduleList)
            {
                calendar.lvShowWorkDay.Items.Add(s);
            }
            var mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWin.frTest.Refresh();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (lvCompanyName.SelectedItem == null)
            {
                MessageBox.Show("Please enter Company name or use Outlook for your personal schedule");
                return;
            }
            ComboBoxItem typeItem = (ComboBoxItem)cbType.SelectedItem;

            String type = "";
            if (cbType.SelectedIndex != -1)
            {
                type = typeItem.Content.ToString();
            }

            String subject = tbSubject.Text;
            String meetingDate = dateOfMetting.Text;
            String startTime = cbStartTime.Text;
            String endTime = cbEndTime.Text;
            String note = tbNote.Text;
            Customer customer = (Customer)lvCompanyName.SelectedItem;

            Schedule schedule = new Schedule();
            schedule.CustomerID = customer.CustomerId;
            schedule.Type = type;
            schedule.Subject = subject;
            schedule.ScheduleDate = DateTime.Parse(meetingDate);
            schedule.StartTime = startTime;
            schedule.EndTime = endTime;
            schedule.Note = note;
            schedule.SalesRepId = Convert.ToInt32(Application.Current.Resources["UserName"]);

            Outlook._Application _app;
            try
            {
                _app = (Outlook.Application)Marshal.GetActiveObject("Outlook.Application");
            }
            // catch (System.Runtime.InteropServices.COMException ex)
            catch (Exception)
            {
                //MessageBox.Show("Outlook is not opened under Administrator, close OUtlook and click OK." + ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                _app = new Outlook.Application();
            }
            try
            {
                Outlook.AppointmentItem oAppointment = (Outlook.AppointmentItem)_app.CreateItem(Outlook.OlItemType.olAppointmentItem);
                oAppointment.Subject = subject;
                oAppointment.Body = note;
                oAppointment.Start = Convert.ToDateTime(meetingDate + " " + startTime);
                oAppointment.End = Convert.ToDateTime(meetingDate + " " + endTime);
                oAppointment.Importance = Outlook.OlImportance.olImportanceHigh;
                oAppointment.ReminderSet = true;
                oAppointment.ReminderMinutesBeforeStart = 30;
                oAppointment.BusyStatus = Outlook.OlBusyStatus.olBusy;
                oAppointment.Save();
                if (type == "Face To Face")
                {
                    Outlook.MailItem mailItem = oAppointment.ForwardAsVcal();
                    mailItem.To = customer.Email;
                    mailItem.Send();
                }



            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("Error sending email" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

            //refresh the list
            var calendar = new Calendar();
            List<Schedule> scheduleList = db.GetAllAppointment();
            calendar.lvShowWorkDay.Items.Clear();
            foreach (Schedule s in scheduleList)
            {
                calendar.lvShowWorkDay.Items.Add(s);
            }
            var mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWin.frTest.Refresh();
        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _errors++;
            else
                _errors--;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dateOfMetting_Loaded(object sender, RoutedEventArgs e)
        {
            dateOfMetting.Text = DateTime.Today.ToShortDateString();
        }

        private void cbStartTime_Loaded(object sender, RoutedEventArgs e)
        {
            cbStartTime.Text = DateTime.Now.AddMinutes(30).ToShortTimeString();
        }

        private void cbEndTime_Loaded(object sender, RoutedEventArgs e)
        {
            cbEndTime.Text = DateTime.Now.AddHours(1).ToShortTimeString();
        }
    }
}
