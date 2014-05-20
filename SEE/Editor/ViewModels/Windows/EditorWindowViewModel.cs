using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Editor.Helpers;
using Editor.Models;
using Editor.Repository;
using Editor.ViewModels.Controls;
using Editor.Views.Controls;
using Editor.Views.Windows;
using Microsoft.Win32;

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

        private void SetNewProject(ScheduleProject proj)
        {
            Project = proj;
            UpdateTables();
        }

        private void UpdateTables()
        {
            _tableController.UpdateTables();
        }

        #endregion

        #region Commands

        public ICommand OpenListsEditorCommand { get { return new DelegateCommand(OnOpenListsEditor, CanExecuteHasActiveProject); } }
        public ICommand OpenGroupsEditorCommand { get { return new DelegateCommand(OnOpenGroupsEditor, CanExecuteHasActiveProject); } }
        public ICommand OpenLecturersEditorCommand { get { return new DelegateCommand(OnOpenLecturersEditor, CanExecuteHasActiveProject); } }
        public ICommand OpenClassroomsEditorCommand { get { return new DelegateCommand(OnOpenClassroomsEditor, CanExecuteHasActiveProject); } }
        public ICommand OpenSpecializationsEditorCommand { get { return new DelegateCommand(OnOpenSpecializationEditor, CanExecuteHasActiveProject); } }
        public ICommand OpenYearsOfStudyEditorCommand { get { return new DelegateCommand(OnOpenYearsOfStudyEditor, CanExecuteHasActiveProject); } }
        public ICommand LoadRandomScheduleCommand { get { return new DelegateCommand(OnLoadRandomSchedule); } }
        public ICommand NewProjectCommand { get { return new DelegateCommand(OnNewProject); } }
        public ICommand SaveProjectCommand { get { return new DelegateCommand(OnSaveProject, CanExecuteHasActiveProject); } }
        public ICommand OpenProjectCommand { get { return new DelegateCommand(OnOpenProject); } }
        public ICommand CalcStatisticCommand { get { return new DelegateCommand(OnCalcStatistic, CanExecuteHasActiveProject); } }
        public ICommand OpenStatisticWindowCommand { get { return new DelegateCommand(OnOpenStatisticWindow, CanExecuteHasActiveProject); } }

        #endregion

        #region Command Handlers


        private void OnCalcStatistic()
        {
            if (Project != null && Project.ClassesSchedule != null)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Project.StatisticCompilation = new StatisticCompilation(Project.ClassesSchedule);
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        private void OnOpenStatisticWindow()
        {
            var vm = new StatisticWindowViewModel(TabCategory.Groups) { Project = Project };
            var window = new StatisticWindow { DataContext = vm };
            window.ShowDialog(); 
        }

        private void OnSaveProject()
        {
            var dlg = new SaveFileDialog
            {
                FileName = "Расписание",
                DefaultExt = ".sch",
                Filter = "Расписание|*.sch|Другие файлы|*.*"
            };
            var result = dlg.ShowDialog();
            if (result == true)
            {
                SaveLoadSchedule.Save(Project.ClassesSchedule, dlg.FileName);
            }
        }

        private void OnOpenProject()
        {
            var dlg = new OpenFileDialog
            {
                FileName = "Расписание",
                DefaultExt = ".sch",
                Filter = "Расписание|*.sch|Другие файлы|*.*"
            };
            var result = dlg.ShowDialog();
            if (result == true)
            {
                var schedule = SaveLoadSchedule.Load(dlg.FileName);
                SetNewProject(new ScheduleProject{ClassesSchedule = schedule});
            }
        }

        private void OnNewProject()
        {
            SetNewProject(new ScheduleProject { ClassesSchedule = new ClassesSchedule() });
        }

        private void OnLoadRandomSchedule()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            _tableController.Tables.Clear();
            SetNewProject(new ScheduleProject { ClassesSchedule = new ScheduleRepository().Schedule });
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void OnOpenGroupsEditor()
        {
            OpenListsEditorHelper(TabCategory.Groups);
        }

        private void OnOpenLecturersEditor()
        {
            OpenListsEditorHelper(TabCategory.Lecturers);
        }

        private void OnOpenClassroomsEditor()
        {
            OpenListsEditorHelper(TabCategory.Classrooms);
        }

        private void OnOpenSpecializationEditor()
        {
            OpenListsEditorHelper(TabCategory.Specializations);
        }

        private void OnOpenYearsOfStudyEditor()
        {
            OpenListsEditorHelper(TabCategory.YearsOfStudy);
        }

        private void OnOpenListsEditor()
        {
            OpenListsEditorHelper();
        }

        private void OpenListsEditorHelper(TabCategory initTab = TabCategory.Groups)
        {
            var vm = new ListsEditWindowViewModel(initTab){Project = Project};
            var window = new ListsEditWindow { DataContext = vm };
            window.ShowDialog();
            UpdateTables();
        }

        private bool CanExecuteHasActiveProject()
        {
            return HasActiveProject;
        }
        
        #endregion
    }
}
