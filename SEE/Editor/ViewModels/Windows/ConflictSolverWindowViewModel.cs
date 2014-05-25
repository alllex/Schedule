using System.Collections.ObjectModel;
using Editor.ViewModels.Cards;

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
        
    }
}
