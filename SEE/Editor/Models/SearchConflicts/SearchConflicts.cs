using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Models.SearchConflicts
{
    using Conflicts = List<Conflict>;

    class SearchConflicts
    {
        public static Conflicts ConflictsGroups(ClassesSchedule schedule)
        {
            var conflicts = new Conflicts();
            conflicts.AddRange(GreatestFourClassesPerDay(schedule));
            return conflicts;
        }

        public static Conflicts GreatestFourClassesPerDay(ClassesSchedule schedule)
        {
            var conflicts = new Conflicts();

            var groupClassesAll = from c in schedule.Classes
                                  group c by new Tuple<Group, Weekdays>(c.Group, c.ClassTime.Day);

            var groupClassesOdd = from g in groupClassesAll
                                  select g.Where(c => c.ClassTime.Week == WeekType.Odd ||
                                                      c.ClassTime.Week == WeekType.Both);
            
            var groupClassesEven = from g in groupClassesAll
                                   select g.Where(c => c.ClassTime.Week == WeekType.Even ||
                                                       c.ClassTime.Week == WeekType.Both);

            foreach (IEnumerable<Class> c in groupClassesOdd)
            {
                if (c.Count() >= 5) conflicts.Add(
                    new Conflict("Больше 4х занятий в день", ConflictType.Warning, c));
            }

            foreach (IEnumerable<Class> c in groupClassesEven)
            {
                if (c.Count() >= 5) conflicts.Add(
                    new Conflict("Больше 4х занятий в день", ConflictType.Warning, c));
            }

            return conflicts;
        }

        public static Conflicts GroupsInDifferentClassrooms(ClassesSchedule schedule)
        {
            return null; // Заглушка
        }
    }
}
