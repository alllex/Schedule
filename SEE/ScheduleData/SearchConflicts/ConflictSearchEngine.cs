using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleData.SearchConflicts
{
    class ConflictSearchEngine
    {
        public static IEnumerable<Conflict> SearchAllConflicts(Schedule schedule)
        {
            var conflicts = new List<Conflict>();
            var allClasses = schedule.ToList();
            conflicts.AddRange(GreaterThanFourClassesPerDay(allClasses));
            conflicts.AddRange(GroupsInDifferentClassrooms(allClasses));
            conflicts.AddRange(LecturersInDifferentClassrooms(allClasses));
            conflicts.AddRange(LecturersOnDifferentClasses(allClasses));
            conflicts.AddRange(NextClassesAtDifferentAddress(allClasses));
            conflicts.AddRange(CardsWithBlankFields(allClasses));

            return conflicts;
        }
        
        public static IEnumerable<Conflict> GreaterThanFourClassesPerDay(Schedule schedule)
        {
            return GreaterThanFourClassesPerDay(schedule.ToList());
        }

        public static IEnumerable<Conflict> GroupsInDifferentClassrooms(Schedule schedule)
        {
            return GroupsInDifferentClassrooms(schedule.ToList());
        }

        public static IEnumerable<Conflict> LecturersInDifferentClassrooms(Schedule schedule)
        {
            return LecturersInDifferentClassrooms(schedule.ToList());
        }

        public static IEnumerable<Conflict> LecturersOnDifferentClasses(Schedule schedule)
        {
            return LecturersOnDifferentClasses(schedule.ToList());
        }

        public static IEnumerable<Conflict> NextClassesAtDifferentAddress(Schedule schedule)
        {
            return NextClassesAtDifferentAddress(schedule.ToList());
        }

        public static IEnumerable<Conflict> CardsWithBlankFields(Schedule schedule)
        {
            return CardsWithBlankFields(schedule.ToList());
        }

        #region Private Methods

        private static IEnumerable<Conflict> GreaterThanFourClassesPerDay(IEnumerable<ClassRecord> allClasses)
        {
            var message = ConflictCompilation.ConflictDescription(ConflictCriteria.GreaterThanFourClassesPerDay);

            return from c in allClasses
                   where c.Group != null && c.ClassTime != null
                   group c by new Tuple<Group, Weekdays>(c.Group, c.ClassTime.Day) into g
                   where g.Count() > 4 
                   select new Conflict(message, ConflictType.Warning, g);
        }

        private static IEnumerable<Conflict> GroupsInDifferentClassrooms(IEnumerable<ClassRecord> allClasses)
        {
            var message = ConflictCompilation.ConflictDescription(ConflictCriteria.GroupsInDifferentClassrooms);

            return from c in allClasses
                   where c.Group != null && c.ClassTime != null
                   group c by new Tuple<Group, ClassTime>(c.Group, c.ClassTime) into g
                   where g.Count() > 1
                   select new Conflict(message, ConflictType.Conflict, g);
        }

        private static IEnumerable<Conflict> LecturersInDifferentClassrooms(IEnumerable<ClassRecord> allClasses)
        {
            var message = ConflictCompilation.ConflictDescription(ConflictCriteria.LecturersInDifferentClassrooms);

            return from c in allClasses
                   where c.Lecturer != null && c.ClassTime != null
                   group c by new Tuple<Lecturer, ClassTime>(c.Lecturer, c.ClassTime) into g
                   where g.Select(c => c.Classroom).Distinct().Count() > 1
                   select new Conflict(message, ConflictType.Conflict, g);
        }


        private static IEnumerable<Conflict> LecturersOnDifferentClasses(IEnumerable<ClassRecord> allClasses)
        {
            var message = ConflictCompilation.ConflictDescription(ConflictCriteria.LecturersOnDifferentClasses);

            return from c in allClasses
                   where c.Lecturer != null && c.ClassTime != null
                   group c by new Tuple<Lecturer, ClassTime>(c.Lecturer, c.ClassTime) into g
                   where g.Select(c => c.Subject).Distinct().Count() > 1
                   select new Conflict(message, ConflictType.Conflict, g);
        }

        private static IEnumerable<Conflict> NextClassesAtDifferentAddress(IEnumerable<ClassRecord> allClasses)
        {
            var conflicts = new List<Conflict>();
            var message = ConflictCompilation.ConflictDescription(ConflictCriteria.NextClassesAtDifferentAddress);

            var classes = from c in allClasses
                          orderby c.Group.Name, c.ClassTime.Day, c.ClassTime.Number
                          select c;

            var prevClass = classes.ElementAt(0);

            for (int i = 1; i < allClasses.Count(); ++i)
            {
                var currClass = classes.ElementAt(i);

                if (currClass.ClassTime != null && prevClass.ClassTime != null &&
                    currClass.Classroom != null && prevClass.Classroom != null &&
                    prevClass.ClassTime.Day == currClass.ClassTime.Day &&
                    currClass.ClassTime.Number - prevClass.ClassTime.Number <= 1 &&
                    prevClass.Classroom.Address != currClass.Classroom.Address)
                {
                    var conflictingClasses = new List<ClassRecord> { prevClass, currClass };
                    conflicts.Add(new Conflict(message, ConflictType.Warning, conflictingClasses));
                }
                prevClass = currClass;
            }
            return conflicts;
        }

        private static IEnumerable<Conflict> CardsWithBlankFields(IEnumerable<ClassRecord> allClasses)
        {
            var message = ConflictCompilation.ConflictDescription(ConflictCriteria.CardsWithBlankFields);

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
