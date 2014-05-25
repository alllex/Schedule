using System.Collections.Generic;

namespace ScheduleData.SearchConflicts
{

    public enum ConflictCriteria
    {
        All,
        GreaterThanFourClassesPerDay,
        GroupsInDifferentClassrooms,
        LecturersInDifferentClassrooms,
        LecterersOnDifferentClasses,        // НОВЫЙ КОНФЛИКТ: ЛЕКТОР ВЕДЁТ РАЗНЫЕ ПРЕДМЕТЫ (ВОЗМОЖНО, В ОДНОЙ АУДИТОРИИ)
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

        public static string ConflictDescription(ConflictCriteria criteria)
        {
            switch (criteria)
            {
                case ConflictCriteria.All:
                    return "Все типы конфликтов";
                case ConflictCriteria.GreaterThanFourClassesPerDay:
                    return "Больше 4х занятий в день";
                case ConflictCriteria.CardsWithBlankFields:
                    return "У карточки не заполнены некоторые поля";
                case ConflictCriteria.GroupsInDifferentClassrooms:
                    return "Группа находится в нескольких аудиториях одновременно";
                case ConflictCriteria.LecturersInDifferentClassrooms:
                    return "Преподаватель находится в нескольких аудиториях одновременно";
                case ConflictCriteria.NextClassesAtDifferentAddress:
                    return "Адреса двух аудиторий, в которых проходят два соседних занятия, различны";
                case ConflictCriteria.LecterersOnDifferentClasses:
                    return "Преподаватель находится проводит несколько занятий одновременно";
            }
            return "";
        }
    }
}
