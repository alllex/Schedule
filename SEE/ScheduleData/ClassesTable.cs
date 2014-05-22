using System.Collections.Generic;
using System.Linq;

namespace ScheduleData
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
            SetGroupIndexes();
            SetTime();
            CreateTable();
        }

        private void SetTime()
        {
            TimeIndexes.Clear();
            for (int i = 0; i < _schedule.TimeLine.Count(); i++)
                TimeIndexes.Add(_schedule.TimeLine[i], i);
        }

        private void SetGroups()
        {
            var gps =
                from g in _schedule.CorrectGroups()
                where g.YearOfStudy == YearOfStudy
                select g;
            Groups = sortGroups(gps);   
        }

        private Group[] sortGroups(IEnumerable<Group> groups)
        {
            var newGroups = new List<Group>(groups.Count());
            var groupsOfGroups = from g in groups
                                 group g by g.Specialization;

            foreach (var g in groupsOfGroups)
                newGroups.AddRange(g);

            return newGroups.ToArray();
        }

        private void SetGroupIndexes()
        {
            GroupIndexes.Clear();
            for (int i = 0; i < Groups.Count(); i++)
                GroupIndexes.Add(Groups[i], i);
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
            return Groups.Count(g => g.Specialization != null);
        }

        public void AddGroup(Group group)
        {
            if (group == null || group.YearOfStudy != YearOfStudy || Groups.Any(g => g == group)) return;

            var oldGroups = Groups.ToList();
            oldGroups.Add(group);
            Groups = sortGroups(oldGroups);
            SetGroupIndexes();

            if (group.Specialization == null) return;
           
            var countGroups = Groups.Length;
            var countTime = TimeIndexes.Count;
            var indexOfNewGroup = GroupIndexes[group];
            var newTable = new ClassRecord[countTime][];

            for (int i = 0; i < countTime; ++i)
            {
                newTable[i] = new ClassRecord[countGroups];
                for (int j = 0; j < countGroups; ++j)
                    if (j != indexOfNewGroup)
                        newTable[i][j] = Table[i][j < indexOfNewGroup ? j : j - 1];
            }
            Table = newTable;
        }

        public void AddGroup(Group group, ClassRecord[] classes)
        {
            AddGroup(group);
            var countTime = TimeIndexes.Count;
            var indexOfGroup = GroupIndexes[group];
            for (int i = 0; i < countTime; ++i)
                Table[i][indexOfGroup] = classes[i];
        }

        public void RemoveGroup(Group group)
        {
            if (group == null || Groups.All(g => g != group)) return;

            var indexOfGroup = GroupIndexes[group];
            var countTime = TimeIndexes.Count;
            var newCountGroups = Groups.Length - 1;
            var newGroups = new Group[newCountGroups];

            for (int j = 0; j < newCountGroups + 1; ++j)
                if (j != indexOfGroup)
                    newGroups[j < indexOfGroup ? j : j - 1] = Groups[j];

            Groups = newGroups;
            SetGroupIndexes();

            var newTable = new ClassRecord[countTime][];

            if (countTime != 0 && indexOfGroup < Table[0].Length)
            {
                for (int i = 0; i < countTime; ++i)
                {
                    newTable[i] = new ClassRecord[newCountGroups];
                    for (int j = 0; j < newCountGroups + 1; ++j)
                        if (j != indexOfGroup)
                            newTable[i][j < indexOfGroup ? j : j - 1] = Table[i][j];
                }

                Table = newTable;
            }
        }

        public ClassRecord[] AllClassesOfGroup(Group group)
        {
            if (group == null || Groups.All(g => g != group)) return null;

            var indexOfGroup = GroupIndexes[group];
            var countTime = TimeIndexes.Count;
            var classes = new ClassRecord[countTime];

            if (countTime != 0 && indexOfGroup < Table[0].Length)
                for (int i = 0; i < countTime; ++i)
                    classes[i] = Table[i][indexOfGroup];

            return classes;
        }
    }
}
