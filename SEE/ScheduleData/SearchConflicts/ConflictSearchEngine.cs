using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleData.SearchConflicts
{
    class ConflictSearchEngine
    {
        public static IEnumerable<Conflict> SearchAllConflicts(ClassesSchedule schedule)
        {
            var conflicts = new List<Conflict>();
            var allClasses = schedule.ToList();
            conflicts.AddRange(GreaterThanFourClassesPerDay(allClasses));
            conflicts.AddRange(GroupsInDifferentClassrooms(allClasses));
            conflicts.AddRange(LecterersInDifferentClassrooms(allClasses));
            conflicts.AddRange(NextClassesAtDifferentAddress(allClasses));
            conflicts.AddRange(CardsWithBlankFields(allClasses));

            return conflicts;
        }
        
        public static IEnumerable<Conflict> GreaterThanFourClassesPerDay(ClassesSchedule schedule)
        {
            return GreaterThanFourClassesPerDay(schedule.ToList());
        }

        public static IEnumerable<Conflict> GroupsInDifferentClassrooms(ClassesSchedule schedule)
        {
            return GroupsInDifferentClassrooms(schedule.ToList());
        }

        public static IEnumerable<Conflict> LecterersInDifferentClassrooms(ClassesSchedule schedule)
        {
            return LecterersInDifferentClassrooms(schedule.ToList());
        }

        public static IEnumerable<Conflict> LecterersOnDifferentClasses(ClassesSchedule schedule)
        {
            return LecterersOnDifferentClasses(schedule.ToList());
        }

        public static IEnumerable<Conflict> NextClassesAtDifferentAddress(ClassesSchedule schedule)
        {
            return NextClassesAtDifferentAddress(schedule.ToList());
        }

        public static IEnumerable<Conflict> CardsWithBlankFields(ClassesSchedule schedule)
        {
            return CardsWithBlankFields(schedule.ToList());
        }

        #region Private Methods

        private static IEnumerable<Conflict> GreaterThanFourClassesPerDay(IEnumerable<FullClassRecord> allClasses)
        {
            var message = "Больше 4х занятий в день";

            return from c in allClasses
                   where c.Group != null && c.Time != null
                   group c by new Tuple<Group, Weekdays>(c.Group, c.Time.Day) into g
                   where g.Count() > 4 
                   select new Conflict(message, ConflictType.Warning, g);
        }

        private static IEnumerable<Conflict> GroupsInDifferentClassrooms(IEnumerable<FullClassRecord> allClasses)
        {
            var message = "Группа находится в нескольких аудиториях одновременно.";

            return from c in allClasses
                   where c.Group != null && c.Time != null
                   group c by new Tuple<Group, ClassTime>(c.Group, c.Time) into g
                   where g.Count() > 1
                   select new Conflict(message, ConflictType.Conflict, g);
        }

        private static IEnumerable<Conflict> LecterersInDifferentClassrooms(IEnumerable<FullClassRecord> allClasses)
        {
            var message = "Преподаватель находится в нескольких аудиториях одновременно.";

            return from c in allClasses
                   where c.Lecturer != null && c.Time != null
                   group c by new Tuple<Lecturer, ClassTime>(c.Lecturer, c.Time) into g
                   where g.Select(c => c.Classroom).Distinct().Count() > 1
                   select new Conflict(message, ConflictType.Conflict, g);
        }


        private static IEnumerable<Conflict> LecterersOnDifferentClasses(IEnumerable<FullClassRecord> allClasses)
        {
            var message = "Преподаватель находится проводит несколько занятий одновременно.";

            return from c in allClasses
                   where c.Lecturer != null && c.Time != null
                   group c by new Tuple<Lecturer, ClassTime>(c.Lecturer, c.Time) into g
                   where g.Select(c => c.Subject).Distinct().Count() > 1
                   select new Conflict(message, ConflictType.Conflict, g);
        }

        private static IEnumerable<Conflict> NextClassesAtDifferentAddress(IEnumerable<FullClassRecord> allClasses)
        {
            var conflicts = new List<Conflict>();
            var message = "Адреса двух аудиторий, в которых проходят два соседних занятия, различны.";

            var classes = from c in allClasses
                          orderby c.Group.Name, c.Time.Day, c.Time.Number
                          select c;

            var prevClass = classes.ElementAt(0);

            for (int i = 1; i < allClasses.Count(); ++i)
            {
                var currClass = classes.ElementAt(i);

                if (currClass.Time != null && prevClass.Time != null &&
                    currClass.Classroom != null && prevClass.Classroom != null &&
                    prevClass.Time.Day == currClass.Time.Day &&
                    currClass.Time.Number - prevClass.Time.Number <= 1 &&
                    prevClass.Classroom.Address != currClass.Classroom.Address)
                {
                    var conflictingClasses = new List<FullClassRecord> { prevClass, currClass };
                    conflicts.Add(new Conflict(message, ConflictType.Warning, conflictingClasses));
                }
                prevClass = currClass;
            }
            return conflicts;
        }

        private static IEnumerable<Conflict> CardsWithBlankFields(IEnumerable<FullClassRecord> allClasses)
        {
            var message = "У этой карточки не заполнены некоторые поля.";

            return from c in allClasses
                   where c.Lecturer == null || c.Classroom == null || c.Subject == null
                   select new Conflict(message, ConflictType.Warning, c);
        }

        #endregion

    }

    // !!! old SearchConflicts !!!
    //class SearchConflicts
    //{
    //    public static Conflicts ConflictsGroups(ClassesSchedule schedule)
    //    {
    //        var conflicts = new Conflicts();
    //        conflicts.AddRange(GreatestFourClassesPerDay(schedule));
    //        return conflicts;
    //    }

    //    public static Conflicts GreatestFourClassesPerDay(ClassesSchedule schedule)
    //    {
    //        var conflicts = new Conflicts();

    //        var groupClassesAll = from c in schedule.Classes
    //                              group c by new Tuple<Group, Weekdays>(c.Group, c.ClassTime.Day);

    //        var groupClassesOdd = from g in groupClassesAll
    //                              select g.Where(c => c.ClassTime.Week == WeekType.Odd ||
    //                                                  c.ClassTime.Week == WeekType.Both);

    //        var groupClassesEven = from g in groupClassesAll
    //                               select g.Where(c => c.ClassTime.Week == WeekType.Even ||
    //                                                   c.ClassTime.Week == WeekType.Both);

    //        foreach (IEnumerable<Class> c in groupClassesOdd)
    //        {
    //            if (c.Count() >= 5) conflicts.Add(
    //                new Conflict("Больше 4х занятий в день", ConflictType.Warning, c));
    //        }

    //        foreach (IEnumerable<Class> c in groupClassesEven)
    //        {
    //            if (c.Count() >= 5) conflicts.Add(
    //                new Conflict("Больше 4х занятий в день", ConflictType.Warning, c));
    //        }

    //        return conflicts;
    //    }

    //    public static Conflicts GroupsInDifferentClassrooms(ClassesSchedule schedule)
    //    {
    //        return null; // Заглушка
    //    }
    //}
}
