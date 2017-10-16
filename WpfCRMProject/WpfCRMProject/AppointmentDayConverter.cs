using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfCRMProject
{

    public class AppointmentDayConverter : IValueConverter
    {
        static Dictionary<DateTime, string> dict = new Dictionary<DateTime, string>();

        public static Dictionary<DateTime, string> Dict
        {
            get { return dict; }
        }

        static AppointmentDayConverter()
        {
            Calendar calendar = new Calendar();
            List<DateTime> date = calendar.showMeetingOnCalendar();
            foreach (DateTime d in date)
            {
                dict.Add(new DateTime(d.Year, d.Month, d.Day), "New aPPointment");
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text;
            if (!dict.TryGetValue((DateTime)value, out text))
                text = null;
            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
