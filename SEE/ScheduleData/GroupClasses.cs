using System.Collections.Generic;
using System.Linq;

namespace ScheduleData
{
    public class GroupClasses : ClassesTable<Group>
    {
        public readonly YearOfStudy YearOfStudy;

        public GroupClasses(Schedule schedule, YearOfStudy yearOfStudy) : base(schedule)
        {
            YearOfStudy = yearOfStudy;
            SetSubjects();
            SetSubjectIndexes();
//            CreateTable();
            CreateTableDictionary();
//            SetClasses();
            SetClassesDictionary();
        }

        private void SetSubjects()
        {
            var gps =
                from g in Schedule.GroupsBySpecialization()
                where g != null && g.YearOfStudy != null && g.Specialization != null
                where g.YearOfStudy == YearOfStudy
                select g;
            Subjects = gps.ToList();
        }

//        private void SetClasses()
//        {
//            var rowsCount = TimeCardsCount();
//            var colsCount = SubjectsCount();
//            for (int i = 0; i < rowsCount; i++)
//            {
//                for (int j = 0; j < colsCount; j++)
//                {
//                    var timeIndex = i;
//                    var subjectIndex = j;
//                    var classes =
//                        from c in Schedule.ClassRecords
//                        where c.ClassTime == Schedule.TimeLine[timeIndex] && c.Group == Subjects[subjectIndex]
//                        select c;
//                    if (classes.Any())
//                    {
//                        Table[timeIndex][subjectIndex] = classes.First();
//                    }
//                }
//            }
//        }

        private void SetClassesDictionary()
        {
            foreach (var subject in Subjects)
            {
                var subject1 = subject;
                foreach (var time in Schedule.TimeLine)
                {
                    var time1 = time;
                    var classes =
                        from c in Schedule.ClassRecords
                        where c.ClassTime == time1 && c.Group == subject1
                        select c;
                    if (classes.Any())
                    {
                        TableDictionary[subject][time] = classes.First();
                    }
                }
            }
        }

        public void AddGroup(Group group)
        {
            if (group.YearOfStudy != YearOfStudy || TableDictionary.ContainsKey(group)) return;
            SetSubjects();
            SetSubjectIndexes();
            TableDictionary[group] = new Dictionary<ClassTime, ClassRecord>();
        }

        public void RemoveGroup(Group group)
        {
            if (group.YearOfStudy != YearOfStudy || !TableDictionary.ContainsKey(group)) return;
            SetSubjects();
            SetSubjectIndexes();
            TableDictionary.Remove(group);
        }

//        public void AddGroup(Group group)
//        {
//            if (group == null || group.YearOfStudy != YearOfStudy || Subjects.Contains(group)) return;
//
//            var oldGroups = Subjects.ToList();
//            oldGroups.Add(group);
//            Subjects = sortGroups(oldGroups);
//            SetSubjectIndexes();
//
//            if (group.Specialization == null) return;
//           
//            var countGroups = Subjects.Length;
//            var countTime = TimeIndexes.Count;
//            var indexOfNewGroup = SubjectIndexes[group];
//            var newTable = new ClassRecord[countTime][];
//
//            for (int i = 0; i < countTime; ++i)
//            {
//                newTable[i] = new ClassRecord[countGroups];
//                for (int j = 0; j < countGroups; ++j)
//                    if (j != indexOfNewGroup)
//                        newTable[i][j] = Table[i][j < indexOfNewGroup ? j : j - 1];
//            }
//            Table = newTable;
//        }
//
//        public void AddGroup(Group group, ClassRecord[] classes)
//        {
//            AddGroup(group);
//            var countTime = TimeIndexes.Count;
//            var indexOfGroup = SubjectIndexes[group];
//            for (int i = 0; i < countTime; ++i)
//                Table[i][indexOfGroup] = classes[i];
//        }
//
//        public void RemoveGroup(Group group)
//        {
//            if (group == null || Subjects.All(g => g != group)) return;
//
//            var indexOfGroup = SubjectIndexes[group];
//            var countTime = TimeIndexes.Count;
//            var newCountGroups = Subjects.Length - 1;
//            var newGroups = new Group[newCountGroups];
//
//            for (int j = 0; j < newCountGroups + 1; ++j)
//                if (j != indexOfGroup)
//                    newGroups[j < indexOfGroup ? j : j - 1] = Subjects[j];
//
//            Subjects = newGroups;
//            SetSubjectIndexes();
//
//            var newTable = new ClassRecord[countTime][];
//
//            if (countTime != 0 && indexOfGroup < Table[0].Length)
//            {
//                for (int i = 0; i < countTime; ++i)
//                {
//                    newTable[i] = new ClassRecord[newCountGroups];
//                    for (int j = 0; j < newCountGroups + 1; ++j)
//                        if (j != indexOfGroup)
//                            newTable[i][j < indexOfGroup ? j : j - 1] = Table[i][j];
//                }
//
//                Table = newTable;
//            }
//        }
//
//        public ClassRecord[] AllClassesOfGroup(Group group)
//        {
//            if (group == null || Subjects.All(g => g != group)) return null;
//
//            var indexOfGroup = SubjectIndexes[group];
//            var countTime = TimeIndexes.Count;
//            var classes = new ClassRecord[countTime];
//
//            if (countTime != 0 && indexOfGroup < Table[0].Length)
//                for (int i = 0; i < countTime; ++i)
//                    classes[i] = Table[i][indexOfGroup];
//
//            return classes;
//        }

        protected override Group GetSubject(ClassRecord classRecord)
        {
            return classRecord.Group;
        }

        protected override void SetSubject(ClassRecord classRecord, Group subject)
        {
            classRecord.Group = subject;
        }
    }
}
