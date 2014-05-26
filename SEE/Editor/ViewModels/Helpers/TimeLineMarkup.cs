using System.Collections.ObjectModel;
using System.Linq;
using Editor.Helpers;
using ScheduleData;

namespace Editor.ViewModels.Helpers
{
    class TimeLineMarkup
    {

        public Collection<TableItem<Weekdays>> Days = new Collection<TableItem<Weekdays>>();
        public Collection<TableItem<ClassTime>> ClassesIntervals = new Collection<TableItem<ClassTime>>(); 

        public TimeLineMarkup(Schedule schedule)
        {
            int currectRow = 0;
            var days =
                (from t in schedule.TimeLine
                 select t.Day).Distinct();

            foreach (var day in days)
            {
                var ls =
                    (from t in schedule.TimeLine
                     where t.Day == day
                     select t).ToList();
                int d = ls.Count();
                if (d == 0) continue;
                Days.Add(new TableItem<Weekdays>(day){Row = currectRow, Column = 0, RowSpan = d});
                foreach (var timeInterval in ls)
                {
                    ClassesIntervals.Add(new TableItem<ClassTime>(timeInterval){Row = currectRow, Column = 1});
                    currectRow++;
                }
            }
        }
    }
}
