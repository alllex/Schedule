using System.Collections.Generic;
using System.Collections.ObjectModel;
using Editor.Helpers;

namespace ScheduleData.SearchConflicts
{

    public enum ConflictCriteria
    {
        All,
        GreaterThanFourClassesPerDay,
        GroupsInDifferentClassrooms,
        LecturersInDifferentClassrooms,
        LecturersOnDifferentClasses,        // НОВЫЙ КОНФЛИКТ: ЛЕКТОР ВЕДЁТ РАЗНЫЕ ПРЕДМЕТЫ (ВОЗМОЖНО, В ОДНОЙ АУДИТОРИИ)
        NextClassesAtDifferentAddress,
        CardsWithBlankFields
    }

    public class ConflictCompilation : NotificationObject
    {
        #region Conflicts

        private ObservableCollection<Conflict> _conflicts = new ObservableCollection<Conflict>();

        public ObservableCollection<Conflict> Conflicts
        {
            get { return _conflicts; }
            set
            {
                if (_conflicts != value)
                {
                    _conflicts = value;
                    RaisePropertyChanged(() => Conflicts);
                }
            }
        }

        #endregion

        
        public void CreateConflictCompilation(Schedule schedule, ConflictCriteria criteria)
        {

            switch (criteria)
            {
                case ConflictCriteria.All:
                    Conflicts = new ObservableCollection<Conflict>(ConflictSearchEngine.SearchAllConflicts(schedule));
                    break;
                case ConflictCriteria.GreaterThanFourClassesPerDay:
                    Conflicts = new ObservableCollection<Conflict>(ConflictSearchEngine.GreaterThanFourClassesPerDay(schedule));
                    break;
                case ConflictCriteria.CardsWithBlankFields:
                    Conflicts = new ObservableCollection<Conflict>(ConflictSearchEngine.CardsWithBlankFields(schedule));
                    break;
                case ConflictCriteria.GroupsInDifferentClassrooms:
                    Conflicts = new ObservableCollection<Conflict>(ConflictSearchEngine.GroupsInDifferentClassrooms(schedule));
                    break;
                case ConflictCriteria.LecturersInDifferentClassrooms:
                    Conflicts = new ObservableCollection<Conflict>(ConflictSearchEngine.LecturersInDifferentClassrooms(schedule));
                    break;
                case ConflictCriteria.LecturersOnDifferentClasses:
                    Conflicts = new ObservableCollection<Conflict>(ConflictSearchEngine.LecturersInDifferentClassrooms(schedule));
                    break;
                case ConflictCriteria.NextClassesAtDifferentAddress:
                    Conflicts = new ObservableCollection<Conflict>(ConflictSearchEngine.NextClassesAtDifferentAddress(schedule));
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
                case ConflictCriteria.LecturersOnDifferentClasses:
                    return "Преподаватель находится проводит несколько занятий одновременно";
            }
            return "";
        }
    }
}
