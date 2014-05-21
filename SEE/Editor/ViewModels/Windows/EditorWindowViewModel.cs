using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using Editor.Repository;
using Editor.ViewModels.Controls;
using Editor.Views.Windows;
using Microsoft.Win32;
using ScheduleData;
using ScheduleData.DataMining;
using ScheduleData.SearchConflicts;

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

        #region ListsEditor commands

        public ICommand OpenListsEditorCommand { get { return new DelegateCommand(OnOpenListsEditor, CanExecuteHasActiveProject); } }
        public ICommand OpenGroupsEditorCommand { get { return new DelegateCommand(OnOpenGroupsEditor, CanExecuteHasActiveProject); } }
        public ICommand OpenLecturersEditorCommand { get { return new DelegateCommand(OnOpenLecturersEditor, CanExecuteHasActiveProject); } }
        public ICommand OpenClassroomsEditorCommand { get { return new DelegateCommand(OnOpenClassroomsEditor, CanExecuteHasActiveProject); } }
        public ICommand OpenSpecializationsEditorCommand { get { return new DelegateCommand(OnOpenSpecializationEditor, CanExecuteHasActiveProject); } }
        public ICommand OpenYearsOfStudyEditorCommand { get { return new DelegateCommand(OnOpenYearsOfStudyEditor, CanExecuteHasActiveProject); } }

        #endregion

        public ICommand LoadRandomScheduleCommand { get { return new DelegateCommand(OnLoadRandomSchedule); } }
        public ICommand NewProjectCommand { get { return new DelegateCommand(OnNewProject); } }
        public ICommand SaveProjectCommand { get { return new DelegateCommand(OnSaveProject, CanExecuteHasActiveProject); } }
        public ICommand OpenProjectCommand { get { return new DelegateCommand(OnOpenProject); } }

        public ICommand CalcStatisticCommand { get { return new DelegateCommand(OnCalcStatistic, CanExecuteHasActiveProject); } }
        public ICommand OpenStatisticWindowCommand { get { return new DelegateCommand(OnOpenStatisticWindow, CanExecuteHasStatistic); } }

        #region Conflicts

        public ICommand CheckAllConflictsCommand { get { return new DelegateCommand(OnCheckCheckAllConflicts, CanExecuteHasActiveProject); } }
        public ICommand CheckConflictGroupsInDifferentClassroomsCommand { get { return new DelegateCommand(OnCheckConflictGroupsInDifferentClassrooms, CanExecuteHasActiveProject); } }
        public ICommand CheckConflictLecturersInDifferentClassroomsCommand { get { return new DelegateCommand(OnCheckConflictLecturersInDifferentClassrooms, CanExecuteHasActiveProject); } }
        public ICommand CheckConflictNextClassesAtDifferentAddressCommand { get { return new DelegateCommand(OnCheckConflictNextClassesAtDifferentAddress, CanExecuteHasActiveProject); } }
        public ICommand CheckConflictCardsWithBlankFieldsCommand { get { return new DelegateCommand(OnCheckConflictCardsWithBlankFields, CanExecuteHasActiveProject); } }
        public ICommand CheckConflictGreaterThanFourClassesPerDayCommand { get { return new DelegateCommand(OnCheckConflictGreaterThanFourClassesPerDay, CanExecuteHasActiveProject); } }
        public ICommand ShowHideConflictsCommand { get { return new DelegateCommand(OnShowHideConflicts, CanExecuteShowHideConflicts); } }

        #endregion

        public ICommand ExportToExcelCommand { get { return new DelegateCommand(OnExportToExcel, CanExecuteHasActiveProject); } }

        #endregion

        #region Command Handlers

        private void OnExportToExcel()
        {
            var dlg = new SaveFileDialog
            {
                FileName = "Расписание",
                DefaultExt = ".xlsx",
                Filter = "Расписание|*.xlsx"
            };
            var result = dlg.ShowDialog();
            if (result == true)
            {
                // export dlg.FileName()
            }
        }

        private void OnCheckCheckAllConflicts()
        {
            CheckConflict(ConflictCriteria.All);
        }

        private void OnCheckConflictGreaterThanFourClassesPerDay()
        {
            CheckConflict(ConflictCriteria.GreaterThanFourClassesPerDay);
        }

        private void OnCheckConflictCardsWithBlankFields()
        {
            CheckConflict(ConflictCriteria.CardsWithBlankFields);
        }

        private void OnCheckConflictNextClassesAtDifferentAddress()
        {
            CheckConflict(ConflictCriteria.NextClassesAtDifferentAddress);
        }

        private void OnCheckConflictLecturersInDifferentClassrooms()
        {
            CheckConflict(ConflictCriteria.LecturersInDifferentClassrooms);
        }

        private void OnCheckConflictGroupsInDifferentClassrooms()
        {
            CheckConflict(ConflictCriteria.GroupsInDifferentClassrooms);
        }

        private void OnShowHideConflicts()
        {
            if (Project.AreConflictsShown)
            {
                _tableController.HideConflicts();
                Project.AreConflictsShown = false;
            }
            else
            {
                if (Project.ConflictCompilation.Conflicts.Any())
                {
                    _tableController.ShowConflicts();
                    Project.AreConflictsShown = true;
                }
                else
                {
                    MessageBox.Show("Ни одного конфликта не найдено!", "Поиск конфликтов");
                }
            }
        }

        private bool CanExecuteShowHideConflicts()
        {
            return HasActiveProject && Project.ConflictCompilation != null;
        }

        private void CheckConflict(ConflictCriteria criteria)
        {
            if (Project != null && Project.ClassesSchedule != null)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                if (Project.AreConflictsShown)
                {
                    OnShowHideConflicts();
                }
                Project.ConflictCompilation = new ConflictCompilation(Project.ClassesSchedule, criteria);
                Mouse.OverrideCursor = Cursors.Arrow;
                if (!Project.AreConflictsShown)
                {
                    OnShowHideConflicts();
                }
            }
        }

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
            var vm = new StatisticWindowViewModel(CalcStatisticCommand) { Project = Project };
            var window = new StatisticWindow { DataContext = vm };
            window.ShowDialog(); 
        }

        private bool CanExecuteHasStatistic()
        {
            return Project != null && Project.StatisticCompilation != null;
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
            var schedule = new ClassesSchedule();
            schedule.InitStdTimeLine();
            SetNewProject(new ScheduleProject { ClassesSchedule = schedule });
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
