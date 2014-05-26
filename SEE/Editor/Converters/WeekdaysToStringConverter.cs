using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Media3D;
using ScheduleData;

namespace Editor.Converters
{

    using Resx = Properties.Resources;

    class WeekdaysToStringConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Weekdays))
            {
                return "";
            }
            switch ((Weekdays) value)
            {
                case Weekdays.Monday:
                    return Resx.WeekdayMonday;
                case Weekdays.Tuesday:
                    return Resx.WeekdayTuesday;
                case Weekdays.Wednesday:
                    return Resx.WeekdayWednesday;
                case Weekdays.Thursday:
                    return Resx.WeekdayThursday;
                case Weekdays.Friday:
                    return Resx.WeekdayFriday;
                case Weekdays.Saturday:
                    return Resx.WeekdaySaturday;
                default:
                    throw new ArgumentOutOfRangeException("value");
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
