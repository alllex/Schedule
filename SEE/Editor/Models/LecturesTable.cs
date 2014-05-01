using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleData;

namespace Editor.Models
{
    class LecturesTable
    {

        private ISchedule _schedule;
        private IEnumerable<IGroup> _groups;
        private IEnumerable<IGroup> _groupsInTable;
        private IEnumerable<ILectureTime> _timeIntervals;

        public SpanedItem<ILecture>[][] Table;

        public LecturesTable(ISchedule schedule, IEnumerable<IGroup> groups)
        {
            _schedule = schedule;
            _groups = groups;
            _groupsInTable = groups; //buildGroupsInTable(groups);
            _timeIntervals = schedule.TimeLine.GetAll();
            CreateTable();
        }

        private IEnumerable<IGroup> buildGroupsInTable(IEnumerable<IGroup> subgroups)
        {
            var found = new List<IGroup>();
            foreach (var subgroup in subgroups)
            {
                found.AddRange(buildGroupsInTable(subgroup.Subgroups));
            }
            return found;
        }

        private void CreateTable()
        {
            int rowsCount = _timeIntervals.Count();
            int colsCount = _groupsInTable.Count();
            int row = 0;
            Table = new SpanedItem<ILecture>[rowsCount][];
            foreach (var timeInterval in _timeIntervals)
            {
                Table[row] = new SpanedItem<ILecture>[colsCount];
                int col = 0;
                foreach (var group in _groupsInTable)
                {
                    Table[row][col] = new SpanedItem<ILecture>(_schedule.Lectures.Get(group, timeInterval));
                    col++;
                }
                row++;
            }
        }

    }
}
