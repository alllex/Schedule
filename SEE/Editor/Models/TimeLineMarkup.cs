using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using ScheduleData.Interfaces;

namespace Editor.Models
{
    class TimeLineMarkup
    {

        public List<TableItem<Weekdays>> Days = new List<TableItem<Weekdays>>();
        public List<TableItem<ITimeInterval>> ClassIntervals = new List<TableItem<ITimeInterval>>();

        public TimeLineMarkup(IEnumerable<ITimeInterval> timeLine)
        {
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
                    ClassIntervals.Add(new TableItem<ITimeInterval>(timeInterval){Row = currectRow, Column = 1});
                    currectRow++;
                }
            }
        }
    }
}
