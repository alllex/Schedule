using System;
using System.ComponentModel;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using Editor.Repository;
using Editor.ViewModels.Controls;
using Editor.Views.Controls;
using Editor.Views.Windows;

namespace Editor.ViewModels.Windows
{

    class EditorWindowViewModel : HasProjectProperty
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
        
        #endregion

        #region Fields

        private readonly TableControllerViewModel _tableController;

        #endregion

        #region Ctor

        public EditorWindowViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        public EditorWindowViewModel(TableControllerViewModel tableControllerViewModel)
        {
            _tableController = tableControllerViewModel;
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var s = sender as EditorWindowViewModel;
            if (s == null) return;
            if (e.PropertyName == "Project")
            {
                HasActiveProject = Project != null;
            }
        }

        #endregion

        #region Methods

        private void UpdateTables()
        {
            //Project.ClassesSchedule.CreateNewTables();
            _tableController.UpdateTables();
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
        public ICommand NewProjectCommand { get { return new DelegateCommand(OnNewProject); } }

        #endregion

        #region Command Handlers

        private void OnNewProject()
        {
            Project = new ScheduleProject {ClassesSchedule = new ClassesSchedule()};
        }

        private void OnLoadData()
        {
            Project = new ScheduleProject { ClassesSchedule = new ScheduleRepository().Schedule };
            UpdateTables();
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
            var vm = new ListsEditWindowViewModel(initTab){Project = Project};
            var window = new ListsEditWindow { DataContext = vm };
            window.ShowDialog();
            UpdateTables();
        }

        #endregion
    }
}
