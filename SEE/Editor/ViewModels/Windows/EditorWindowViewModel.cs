using System.Diagnostics;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using Editor.Repository;
using Editor.ViewModels.Controls;
using Editor.Views;

namespace Editor.ViewModels
{

    class EditorWindowViewModel : BaseViewModel
    {

        #region Properties

        #region HasActiveProject

        private bool _hasActiveProject;

        public bool HasActiveProject
        {
            get { return _hasActiveProject; }
            set
            {
                if (_hasActiveProject != value)
                {
                    _hasActiveProject = value;
                    RaisePropertyChanged(() => HasActiveProject);
                }
            }
        }

        #endregion

        protected override void ClassesScheduleOnPropertyChanged()
        {
            if (TablesControllerDataContext == null)
            {
                TablesControllerDataContext = new TablesControllerViewModel { ClassesSchedule = ClassesSchedule };
            }
            HasActiveProject = ClassesSchedule != null;
        }

        #region TablesControllerDataContext

        private TablesControllerViewModel _tableControllerDataContext;

        public TablesControllerViewModel TablesControllerDataContext
        {
            get { return _tableControllerDataContext; }
            set
            {
                if (_tableControllerDataContext != value)
                {
                    _tableControllerDataContext = value;
                    RaisePropertyChanged(() => TablesControllerDataContext);
                }
            }
        }

        #endregion

        #endregion

        #region Ctor

        public EditorWindowViewModel()
        {
            OnLoadData();
        }

        #endregion

        #region Commands

        public ICommand OpenListsEditorCommand { get { return new DelegateCommand(OnOpenListsEditor); } }
        public ICommand OpenGroupsEditorCommand { get { return new DelegateCommand(OnOpenGroupsEditor); } }
        public ICommand OpenLecturersEditorCommand { get { return new DelegateCommand(OnOpenLecturersEditor); } }
        public ICommand OpenClassroomsEditorCommand { get { return new DelegateCommand(OnOpenClassroomsEditor); } }
        public ICommand OpenSpecializationsEditorCommand { get { return new DelegateCommand(OnOpenSpecializationEditor); } }
        public ICommand OpenYearsOfStudyEditorCommand { get { return new DelegateCommand(OnOpenYearsOfStudyEditor); } }
        public ICommand LoadDataCommand { get { return new DelegateCommand(OnLoadData); } }

        #endregion

        #region Command Handlers

        private void OnLoadData()
        {
            ClassesSchedule = new ScheduleRepository().Schedule;
        }

        private void OnOpenGroupsEditor()
        {
            OpenListsEditorHelper(ListsEditorTab.Groups);
        }

        private void OnOpenLecturersEditor()
        {
            OpenListsEditorHelper(ListsEditorTab.Lecturers);
        }

        private void OnOpenClassroomsEditor()
        {
            OpenListsEditorHelper(ListsEditorTab.Classrooms);
        }

        private void OnOpenSpecializationEditor()
        {
            OpenListsEditorHelper(ListsEditorTab.Specializations);
        }

        private void OnOpenYearsOfStudyEditor()
        {
            OpenListsEditorHelper(ListsEditorTab.YearsOfStudy);
        }

        private void OnOpenListsEditor()
        {
            OpenListsEditorHelper();
        }

        private void OpenListsEditorHelper(ListsEditorTab initTab = ListsEditorTab.Groups)
        {
            var vm = new ListsEditWindowViewModel(ClassesSchedule, initTab);
            var window = new ListsEditWindow { DataContext = vm };
            window.ShowDialog();
        }

        #endregion
    }
}
