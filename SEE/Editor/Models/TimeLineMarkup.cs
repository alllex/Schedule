using System.Collections.ObjectModel;
using System.Linq;
using Editor.Helpers;

namespace Editor.Models
{
    class TimeLineMarkup
    {

        public Collection<TableItem<Weekdays>> Days = new Collection<TableItem<Weekdays>>();
        public Collection<TableItem<TimeInterval>> ClassesIntervals = new Collection<TableItem<TimeInterval>>();

        public TimeLineMarkup(ClassesSchedule classesSchedule)
        {
            var cts =
                (from t in classesSchedule.TimeLine
                 group t by t.Week into bd
                 select bd).First();
            var timeLine = cts.Cast<TimeInterval>().ToArray();
            int currectRow = 0;
            var days =
                (from t in timeLine
                 select t.Day).Distinct();

            foreach (var day in days)
            {
                var ls =
                    (from t in timeLine
                     where t.Day == day
                     select t).ToList();
                int d = ls.Count();
                if (d == 0) continue;
                Days.Add(new TableItem<Weekdays>(day){Row = currectRow, Column = 0, RowSpan = d});
                foreach (var timeInterval in ls)
                {
                    ClassesIntervals.Add(new TableItem<TimeInterval>(timeInterval){Row = currectRow, Column = 1});
                    currectRow++;
                }
            }
        }
    }
}
