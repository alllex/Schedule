﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Models.SearchConflicts
{
    using Conflicts    = List<Conflict>;
    using ScheduleList = List<FullClassRecord>; 

    class SearchConflicts
    {
        public static Conflicts SearchAllConflicts(ClassesSchedule schedule)
        {
            var conflicts = new Conflicts();
            var allClasses = schedule.ToList();
            conflicts.AddRange(GreaterFourClassesPerDay(allClasses));
            conflicts.AddRange(GroupsInDifferentClassrooms(allClasses));
            conflicts.AddRange(LecterersInDifferentClassrooms(allClasses));
            conflicts.AddRange(NextClassesAtDifferentAddress(allClasses));
            return conflicts;
        }

        
        public static Conflicts GreaterFourClassesPerDay(ClassesSchedule schedule)
        {
            return GreaterFourClassesPerDay(schedule.ToList());
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

        private static Conflicts GreaterFourClassesPerDay(ScheduleList allClasses)
        {
            var conflicts = new Conflicts();
            var message = "Больше 4х занятий в день";

            var groupClasses = from c in allClasses
                               group c by new Tuple<Group, Weekdays>(c.Group, c.Time.Day);

            foreach (var c in groupClasses)
                if (c.Count() >= 5) conflicts.Add(new Conflict(message, ConflictType.Warning, c));

            return conflicts;
        }

        private static Conflicts GroupsInDifferentClassrooms(ScheduleList allClasses)
        {
            var conflicts = new Conflicts();
            var message = "Группа находится в нескольких аудиториях одновременно.";

            var groupClasses = from c in allClasses
                               group c by new Tuple<Group, ClassTime>(c.Group, c.Time);

            foreach (var c in groupClasses)
                if (c.Count() > 1) conflicts.Add(new Conflict(message, ConflictType.Conflict, c));

            return conflicts;
        }

        private static Conflicts LecterersInDifferentClassrooms(ScheduleList allClasses)
        {
            var conflicts = new Conflicts();
            var message = "Преподаватель находится в нескольких аудиториях одновременно.";

            var groupClasses = from c in allClasses
                               group c by new Tuple<Lecturer, ClassTime>(c.Lecturer, c.Time);

            foreach (var c in groupClasses)
                if (c.Count() > 1) conflicts.Add(new Conflict(message, ConflictType.Conflict, c));

            return conflicts;
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
                if (prevClass.Time.Day == currClass.Time.Day &&
                    currClass.Time.Number - prevClass.Time.Number <= 1 &&
                    prevClass.Classroom.Address != currClass.Classroom.Address)
                {
                    var conflictingClasses = new List<FullClassRecord> {prevClass, currClass};
                    conflicts.Add(new Conflict(message, ConflictType.Warning, conflictingClasses));
                }
                        
            }
            return conflicts;
        }

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
