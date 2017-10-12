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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfCRMProject.Domain;

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for EditAppointment.xaml
    /// </summary>
    public partial class EditAppointment : Window
    {
        Database db;
        public EditAppointment()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Schedule schedule = new Schedule();
            schedule.Note = tbNote.Text;
            schedule.ScheduleDate = Convert.ToDateTime(dpDate.Text);
            schedule.StartTime = tpStart.Text;
            schedule.EndTime = tpEnd.Text;

            ComboBoxItem comboState= (ComboBoxItem)cmbState.SelectedItem;
            schedule.Status = comboState.Content.ToString();
            schedule.Schedule_id = Convert.ToInt32(lblScheduleID.Content.ToString());
            try
            {
                db = new Database();
                db.UpdateAppointment(schedule);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            
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
    }
}
