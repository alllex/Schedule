using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using ScheduleData;

namespace Editor.Models
{
    class TimeLineMarkup
    {

        public List<TableItem<Weekdays>> Days = new List<TableItem<Weekdays>>();
        public List<TableItem<ITimeInterval>> LectureIntervals = new List<TableItem<ITimeInterval>>();

        public TimeLineMarkup(IEnumerable<ITimeInterval> timeLine)
        {
            int currectRow = 0;
            var days = Enum.GetValues(typeof(Weekdays));
            for (int i = 0; i < days.Length; i++)
            {
                var day = (Weekdays) i;
                var ls =
                    (from t in timeLine
                    where t.Day == day
                    select t).ToList();
                int d = ls.Count();
                Days.Add(new TableItem<Weekdays>(day){Row = currectRow, Column = 0, RowSpan = d});
                foreach (var timeInterval in ls)
                {
                    LectureIntervals.Add(new TableItem<ITimeInterval>(timeInterval){Row = currectRow, Column = 1});
                    currectRow++;
                }
            }
        }
    }
}
