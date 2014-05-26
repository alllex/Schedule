using System.Collections.ObjectModel;
using Editor.Helpers;

namespace ScheduleData.SearchConflicts
{
    
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
                case ConflictCriteria.MoreThanFourClassesPerDay:
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
                    return Properties.Resources.ConflictCriteriaAll;
                case ConflictCriteria.MoreThanFourClassesPerDay:
                    return Properties.Resources.ConflictCriteriaMoreThanFourClassesPerDay;
                case ConflictCriteria.CardsWithBlankFields:
                    return Properties.Resources.ConflictCriteriaCardsWithBlankFields;
                case ConflictCriteria.GroupsInDifferentClassrooms:
                    return Properties.Resources.ConflictCriteriaGroupsInDifferentClassrooms;
                case ConflictCriteria.LecturersInDifferentClassrooms:
                    return Properties.Resources.ConflictCriteriaLecturersInDifferentClassrooms;
                case ConflictCriteria.NextClassesAtDifferentAddress:
                    return Properties.Resources.ConflictCriteriaNextClassesAtDifferentAddress;
                case ConflictCriteria.LecturersOnDifferentClasses:
                    return Properties.Resources.ConflictCriteriaLecturersOnDifferentClasses;
            }
            return "";
        }
    }
}
