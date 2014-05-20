﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Editor.Models.SearchConflicts
{
    using Conflicts    = List<Conflict>;
    using ScheduleList = List<FullClassRecord>; 

    class ConflictSearchEngine
    {
        public static Conflicts SearchAllConflicts(ClassesSchedule schedule)
        {
            var conflicts = new Conflicts();
            var allClasses = schedule.ToList();
            conflicts.AddRange(GreaterThanFourClassesPerDay(allClasses));
            conflicts.AddRange(GroupsInDifferentClassrooms(allClasses));
            conflicts.AddRange(LecterersInDifferentClassrooms(allClasses));
            conflicts.AddRange(NextClassesAtDifferentAddress(allClasses));
            conflicts.AddRange(CardsWithBlankFields(allClasses));

            return conflicts;
        }
        
        public static Conflicts GreaterThanFourClassesPerDay(ClassesSchedule schedule)
        {
            return GreaterThanFourClassesPerDay(schedule.ToList());
        }

        public static Conflicts GroupsInDifferentClassrooms(ClassesSchedule schedule)
        {
            return GroupsInDifferentClassrooms(schedule.ToList());
        }

        public static Conflicts LecterersInDifferentClassrooms(ClassesSchedule schedule)
        {
            return LecterersInDifferentClassrooms(schedule.ToList());
        }

        public static Conflicts NextClassesAtDifferentAddress(ClassesSchedule schedule)
        {
            return NextClassesAtDifferentAddress(schedule.ToList());
        }

        public static Conflicts CardsWithBlankFields(ClassesSchedule schedule)
        {
            return CardsWithBlankFields(schedule.ToList());
        }

        #region Private Methods

        private static Conflicts GreaterThanFourClassesPerDay(ScheduleList allClasses)
        {
            var message = "Больше 4х занятий в день";

            var groupClasses = from c in allClasses
                               group c by new Tuple<Group, Weekdays>(c.Group, c.Time.Day);

            return (from c in groupClasses where c.Count() > 4 select new Conflict(message, ConflictType.Warning, c)).ToList();
        }

        private static Conflicts GroupsInDifferentClassrooms(ScheduleList allClasses)
        {
            var message = "Группа находится в нескольких аудиториях одновременно.";

            var groupClasses = from c in allClasses
                               group c by new Tuple<Group, ClassTime>(c.Group, c.Time);

            return (from c in groupClasses where c.Count() > 1 select new Conflict(message, ConflictType.Conflict, c)).ToList();
        }

        private static Conflicts LecterersInDifferentClassrooms(ScheduleList allClasses)
        {
            var message = "Преподаватель находится в нескольких аудиториях одновременно.";

            var groupClasses = from c in allClasses
                               group c by new Tuple<Lecturer, ClassTime>(c.Lecturer, c.Time);

            return (from c in groupClasses where c.Count() > 1 select new Conflict(message, ConflictType.Conflict, c)).ToList();
        }

        private static Conflicts NextClassesAtDifferentAddress(ScheduleList allClasses)
        {
            var conflicts = new Conflicts();
            var message = "Адреса двух аудиторий, в которых проходят два соседних занятия, различны.";

            var classes = from c in allClasses
                          orderby c.Group.Name, c.Time.Day, c.Time.Number
                          select c;

            var prevClass = classes.ElementAt(0);

            for (int i = 1; i < allClasses.Count; ++i)
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
            }
            return conflicts;
        }

        private static Conflicts CardsWithBlankFields(ScheduleList allClasses)
        {
            var message = "У этой карточки не заполнены некоторые поля.";

            return (from c in allClasses
                    where c.Lecturer == null || c.Classroom == null || c.Subject == null
                    select new Conflict(message, ConflictType.Warning, c)).ToList();
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
