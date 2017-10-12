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

/* default show today task/schedule.
 * right click contect menu to see the detail(note), change status, change date
 * add button, to create new schedule, with the type (phone, visit, email), search company name textbox and search icon,
 * --> display result, selected, OK copy the company_id
 * --> task (to do related to company), appointment no email, meeting send invitation (app n meeting need start and end time)
 * --> todo: create view to link schedule and message
 * when status is completed copy to customer history
 * (salesrep inner join schedule) inner join (customer inner join message)
 */

namespace WpfCRMProject
{
    /// <summary>
    /// Interaction logic for WorkDay.xaml
    /// </summary>
    public partial class WorkDay : Page
    {
        public WorkDay()
        {
            InitializeComponent();
        }
    }
}

