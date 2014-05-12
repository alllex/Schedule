using System.Collections.Generic;
using System.Linq;

namespace Editor.Models
{
    class ClassesTable
    {

        private readonly ClassesSchedule _schedule;
        private readonly YearOfStudy _year;
        private Dictionary<TimeInterval, int> _timeIndex;

        public Group[] Groups;
        public TimeInterval[] TimeIntervals;
        public SpanedItem<Class>[][] Table;

        public ClassesTable(ClassesSchedule schedule, YearOfStudy year)
        {
            _schedule = schedule;
            _year = year;
            SetGroups();
            SetTimeIntervals();
            SetIndexes();
            CreateTable();
        }

        private void SetIndexes()
        {
            SetTimeIndex();
        }

        private void SetTimeIndex()
        {
            _timeIndex = new Dictionary<TimeInterval, int>();
            for (int i = 0; i < TimeIntervals.Length; i++)
            {
                _timeIndex.Add(TimeIntervals[i], i);
            }
        }

        private void SetGroups()
        {
            var gps = 
                from g in _schedule.Groups
                where g.YearOfStudy.Name == _year.Name
                orderby g.Specialization, g.Name
                select g;
            Groups = gps.ToArray();
        }

        private void SetTimeIntervals()
        {
            var cts =
                (from t in _schedule.TimeLine
                  group t by t.Week into bd
                  select bd).First();
            TimeIntervals = cts.Cast<TimeInterval>().ToArray();
        }

        private void CreateTable()
        {
            int rowsCount = TimeIntervals.Count();
            int colsCount = Groups.Count();
            Table = new SpanedItem<Class>[rowsCount][];
            for (int i = 0; i < rowsCount; i++)
            {
                Table[i] = new SpanedItem<Class>[colsCount];
            }
            foreach (var classTime in _schedule.TimeLine)
            {
                ClassTime time = classTime;
                int row;
                if (!_timeIndex.TryGetValue(classTime, out row)) continue;
                for (var col = 0; col < colsCount; col++)
                {
                    var cs = _schedule.Classes.Where(@class => @class.ClassTime.Equals(time) && @class.Group.Equals(Groups[col]));
                    var enumerable = cs as IList<Class> ?? cs.ToList();
                    Table[row][col] = new SpanedItem<Class>(enumerable.Any() ? enumerable.First() : null);
                }
            }
        }

        public int RowsCount()
        {
            return TimeIntervals.Count();
        }

        public int ColumnsCount()
        {
            return Groups.Count();
        }

    }
}
