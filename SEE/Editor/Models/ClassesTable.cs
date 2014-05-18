using System.Linq;

namespace Editor.Models
{
    public class ClassesTable
    {
        private readonly ClassesSchedule _schedule;
        public readonly YearOfStudy YearOfStudy;

        public Group[] Groups;
        public ClassRecord[][] Table;

        public ClassesTable(ClassesSchedule schedule, YearOfStudy yearOfStudy)
        {
            _schedule = schedule;
            YearOfStudy = yearOfStudy;
            SetGroups();
            CreateTable();
        }

        private void SetGroups()
        {
            var gps = 
                from g in _schedule.Groups
                where g.YearOfStudy.Name == YearOfStudy.Name
                orderby g.Specialization, g.Name
                select g;
            Groups = gps.ToArray();
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
