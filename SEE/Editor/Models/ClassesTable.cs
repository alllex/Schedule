using System.Collections.Generic;
using System.Linq;
using ScheduleData.Editor;
using ScheduleData.Interfaces;

namespace Editor.Models
{
    class ClassesTable
    {

        private readonly ISchedule _schedule;
        private readonly IYearOfStudy _year;
        private Dictionary<ITimeInterval, int> _timeIndex;
        private Dictionary<IGroup, int> _groupIndex;

        public IGroup[] Groups;
        public ITimeInterval[] TimeIntervals;
        public SpanedItem<IClass>[][] Table;

        public ClassesTable(ISchedule schedule, IYearOfStudy year)
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
            SetGroupIndex();
        }

        private void SetTimeIndex()
        {
            _timeIndex = new Dictionary<ITimeInterval, int>();
            for (int i = 0; i < TimeIntervals.Length; i++)
            {
                _timeIndex.Add(TimeIntervals[i], i);
            }
        }

        private void SetGroupIndex()
        {
            _groupIndex = new Dictionary<IGroup, int>();
            for (int i = 0; i < Groups.Length; i++)
            {
                _groupIndex.Add(Groups[i], i);
            }
        }

        private void SetGroups()
        {
            var gps = 
                from g in _schedule.Groups.GetAll().ToArray()
                where g.YearOfStudy == _year 
                orderby g.Specialization, g.Name
                select g;
            Groups = gps.ToArray();
        }

        private void SetTimeIntervals()
        {
            var cts =
                (from t in _schedule.TimeLine.GetAll()
                 orderby t.Day, t.Begin, t.End
                 select (ITimeInterval)(new TimeInterval(t))).Distinct();
            int i = 0;
            TimeIntervals = new ITimeInterval[cts.Count()];
            foreach (var timeInterval in cts)
            {
                TimeIntervals[i++] = timeInterval;
            }
        }

        private void CreateTable()
        {
            int rowsCount = TimeIntervals.Count();
            int colsCount = Groups.Count();
            Table = new SpanedItem<IClass>[rowsCount][];
            for (int i = 0; i < rowsCount; i++)
            {
                Table[i] = new SpanedItem<IClass>[colsCount];
            }
            foreach (var classTime in _schedule.TimeLine.GetAll())
            {
                int row;
                _timeIndex.TryGetValue(new TimeInterval(classTime), out row);
                for (int col = 0; col < colsCount; col++)
                {
                    Table[row][col] = new SpanedItem<IClass>(_schedule.Classes.Get(Groups[col], classTime));
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
