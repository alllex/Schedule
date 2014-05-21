using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleData
{
    public enum WeekType
    {
        Odd,
        Even,
        Both
    }

    public enum Weekdays
    {
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday //, Sunday
    }

    public class Time
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public override string ToString()
        {
            string h = Hours.ToString(CultureInfo.InvariantCulture);
            if (Hours < 10) h = "0" + h;
            string m = Minutes.ToString(CultureInfo.InvariantCulture);
            if (Minutes < 10) m = "0" + m;
            return String.Format("{0}:{1}", h, m);
        }
    }


}
