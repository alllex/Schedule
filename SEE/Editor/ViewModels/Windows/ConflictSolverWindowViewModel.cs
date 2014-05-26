using System.Collections.ObjectModel;
using Editor.ViewModels.Cards;
using ScheduleData.SearchConflicts;

namespace Editor.ViewModels.Windows
{
    class ConflictSolverWindowViewModel : HasProjectProperty
    {

        #region ConflictedClasses

        private ObservableCollection<ClassCardViewModel> _conflictedClasses = new ObservableCollection<ClassCardViewModel>();

        public ObservableCollection<ClassCardViewModel> ConflictedClasses
        {
            get { return _conflictedClasses; }
            set
            {
                if (_conflictedClasses != value)
                {
                    _conflictedClasses = value;
                    RaisePropertyChanged(() => ConflictedClasses);
                }
            }
        }

        #endregion

        #region Conflict

        private Conflict _conflict;

        public Conflict Conflict
        {
            get { return _conflict; }
            set
            {
                if (_conflict != value)
                {
                    _conflict = value;
                    RaisePropertyChanged(() => Conflict);
                }
            }
        }

        #endregion

        
        
    }
}
