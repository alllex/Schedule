using System.Collections.Generic;
using System.Linq;

namespace Editor.Models
{
    public class ClassesTable
    {
        private readonly ClassesSchedule _schedule;
        public readonly YearOfStudy YearOfStudy;

        public Group[] Groups;
        public Dictionary<Group, int> GroupIndexes = new Dictionary<Group, int>();
        public Dictionary<ClassTime, int> TimeIndexes = new Dictionary<ClassTime, int>();
        public ClassRecord[][] Table;

        public ClassesTable(ClassesSchedule schedule, YearOfStudy yearOfStudy)
        {
            _schedule = schedule;
            YearOfStudy = yearOfStudy;
            SetGroups();
            SetTime();
            CreateTable();
        }

        private void SetTime()
        {
            for (int i = 0; i < _schedule.TimeLine.Count(); i++)
            {
                TimeIndexes.Add(_schedule.TimeLine[i], i);
            }
        }

        private void SetGroups()
        {
            var gps =
                from g in _schedule.Groups
                where g.YearOfStudy.Name == YearOfStudy.Name
                select g;
            Groups = gps.ToArray();
            for (int i = 0; i < Groups.Count(); i++)
            {
                GroupIndexes.Add(Groups[i], i);
            }
        }

        private void CreateTable()
        {
            int rowsCount = _schedule.TimeLine.Count();
            int colsCount = Groups.Count();
            Table = new ClassRecord[rowsCount][];
            for (int i = 0; i < rowsCount; i++)
            {
                Table[i] = new ClassRecord[colsCount];
            }
        }

        public int RowsCount()
        {
            return _schedule.TimeLine.Count();
        }

        public int ColumnsCount()
        {
            return Groups.Count();
        }

    }
}
