using System.Collections.Generic;

namespace ScheduleData.SearchConflicts
{

    public enum ConflictCriteria
    {
        All,
        GreaterThanFourClassesPerDay,
        GroupsInDifferentClassrooms,
        LecturersInDifferentClassrooms,
        NextClassesAtDifferentAddress,
        CardsWithBlankFields
    }

    public class ConflictCompilation
    {
        public IEnumerable<Conflict> Conflicts;

        public ConflictCompilation(Schedule schedule, ConflictCriteria criteria)
        {
            switch (criteria)
            {
                case ConflictCriteria.All:
                    Conflicts = ConflictSearchEngine.SearchAllConflicts(schedule);
                    break;
                case ConflictCriteria.GreaterThanFourClassesPerDay:
                    Conflicts = ConflictSearchEngine.GreaterThanFourClassesPerDay(schedule);
                    break;
                case ConflictCriteria.CardsWithBlankFields:
                    Conflicts = ConflictSearchEngine.CardsWithBlankFields(schedule);
                    break;
                case ConflictCriteria.GroupsInDifferentClassrooms:
                    Conflicts = ConflictSearchEngine.GroupsInDifferentClassrooms(schedule);
                    break;
                case ConflictCriteria.LecturersInDifferentClassrooms:
                    Conflicts = ConflictSearchEngine.LecterersInDifferentClassrooms(schedule);
                    break;
                case ConflictCriteria.NextClassesAtDifferentAddress:
                    Conflicts = ConflictSearchEngine.NextClassesAtDifferentAddress(schedule);
                    break;
            }
        }
    }
}
