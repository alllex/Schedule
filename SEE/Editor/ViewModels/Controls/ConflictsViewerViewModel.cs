using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Editor.Helpers;
using Editor.ViewModels.Cards;
using Editor.ViewModels.Windows;
using Editor.Views.Windows;
using ScheduleData;
using ScheduleData.SearchConflicts;

namespace Editor.ViewModels.Controls
{
    class ConflictsViewerViewModel : HasProjectProperty
    {

        public ICommand OpenConflictSolverCommand { get { return new DelegateCommand(OnOpenConflictSolver); } }

        private void OnOpenConflictSolver(object obj)
        {
            var conf = obj as Conflict;
            if (conf == null) return;
            var vm = new ConflictSolverWindowViewModel
            {
                Project = Project,
                ConflictedClasses = new ObservableCollection<ClassCardViewModel>(from c in conf.ConflictingClasses select (new ClassCardViewModel(c)))
            };
            var wind = new ConflictSolverWindow{DataContext = vm};
            wind.ShowDialog();
        }
    }
}
