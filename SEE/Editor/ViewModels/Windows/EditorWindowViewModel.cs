using System;
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

    using Resx = Properties.Resources;

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
        private readonly ConflictsViewerViewModel _conflictsViewer;

        #endregion

        #region Ctor

        public EditorWindowViewModel(TableControllerViewModel tableControllerViewModel, ConflictsViewerViewModel conflictsViewerViewModel)
        {
            _tableController = tableControllerViewModel;
            _conflictsViewer = conflictsViewerViewModel;
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
            Project.ScheduleController = new ScheduleController { Project = Project };
            Project.ConflictCompilation = new ConflictCompilation();
            AddUpdaters();
            UpdateAll();
        }

        private void AddUpdaters()
        {
            Project.ScheduleController.AddYearOfStudyDelegate += AddYearOfStudy;
            Project.ScheduleController.RemoveYearOfStudyDelegate += RemoveYearOfStudy;
            Project.ScheduleController.AddSpecializationDelegate += AddSpecialization;
            Project.ScheduleController.RemoveSpecializationDelegate += RemoveSpecialization;
            Project.ScheduleController.AddGroupDelegate += AddGroup;
            Project.ScheduleController.RemoveGroupDelegate += RemoveGroup;
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
        public ICommand CheckConflictLecturersOnDifferentClassesCommand { get { return new DelegateCommand(OnCheckConflictLecturersOnDifferentClasses, CanExecuteHasActiveProject); } }
        public ICommand ShowHideConflictsCommand { get { return new DelegateCommand(OnShowHideConflicts, CanExecuteShowHideConflicts); } }

        #endregion

        public ICommand ExportToExcelCommand { get { return new DelegateCommand(OnExportToExcel, CanExecuteHasActiveProject); } }
        public ICommand ImportFromExcelCommand { get { return new DelegateCommand(OnImportFromExcel); } }
        public ICommand ExportToDatabaseCommand { get { return new DelegateCommand(OnExportToDatabase, CanExecuteHasActiveProject); } }
        public ICommand ImportFromDatabaseCommand { get { return new DelegateCommand(OnImportFromDatabase); } }

        #endregion

        #region Command Handlers

        private void OnImportFromDatabase()
        {
            var fileName = Resx.ProjectFileDefaultName;
            var fileExt = Resx.ProjectFileExtensionDatabase;
            var filter = Resx.ProjectFileFilterDatabase;
            var dlg = new OpenFileDialog
            {
                FileName = fileName,
                DefaultExt = "." + fileExt,
                Filter = filter + "|*." + fileExt
            };
            var result = dlg.ShowDialog();
            if (result == true)
            {
                var schedule = ScheduleImporter.DatabaseImport(dlg.FileName);
                SetNewProject(new ScheduleProject { Schedule = schedule });
            }
            UpdateStatus("Импорт выполнен");
        }

        private void OnExportToDatabase()
        {
            var fileName = Resx.ProjectFileDefaultName;
            var fileExt = Resx.ProjectFileExtensionDatabase;
            var filter = Resx.ProjectFileFilterDatabase;
            var dlg = new SaveFileDialog
            {
                FileName = fileName,
                DefaultExt = "." + fileExt,
                Filter = filter + "|*." + fileExt
            };
            var result = dlg.ShowDialog();
            if (result == true)
            {
                ScheduleExporter.DatabaseExport(Project.Schedule, dlg.FileName);
            }
            UpdateStatus("Экспорт выполнен");
        }

        private void OnImportFromExcel()
        {
            var fileName = Resx.ProjectFileDefaultName;
            var fileExt = Resx.ProjectFileExtensionExcel;
            var filter = Resx.ProjectFileFilterExcel;
            var dlg = new OpenFileDialog
            {
                FileName = fileName,
                DefaultExt = "." + fileExt,
                Filter = filter + "|*." + fileExt
            };
            var result = dlg.ShowDialog();
            if (result == true)
            {
                Action action = () =>
                {
                    var schedule = ScheduleImporter.ExcelImport(dlg.FileName);
                    SetNewProject(new ScheduleProject {Schedule = schedule});
                };
                ExecuteAction("Импорт", "Импорт выполнен", action);
            }
        }

        private void OnExportToExcel()
        {
            var fileName = Resx.ProjectFileDefaultName;
            var fileExt = Resx.ProjectFileExtensionExcel;
            var filter = Resx.ProjectFileFilterExcel;
            var dlg = new SaveFileDialog
            {
                FileName = fileName,
                DefaultExt = "." + fileExt,
                Filter = filter + "|*." + fileExt
            };
            var result = dlg.ShowDialog();
            if (result == true)
            {
                Action action = () => ScheduleExporter.ExcelExport(Project.Schedule, dlg.FileName);
                ExecuteAction("Экспорт", "Экспорт выполнен", action);
            }
        }

        private void OnCheckCheckAllConflicts()
        {
            CheckConflict(ConflictCriteria.All);
        }

        private void OnCheckConflictGreaterThanFourClassesPerDay()
        {
            CheckConflict(ConflictCriteria.MoreThanFourClassesPerDay);
        }

        private void OnCheckConflictLecturersOnDifferentClasses()
        {
            CheckConflict(ConflictCriteria.LecturersOnDifferentClasses);
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
                    MessageBox.Show(Resx.MsgBoxMessageNoConflicts, Resx.MsgBoxHeaderNoConflicts);
                }
            }
        }

        private bool CanExecuteShowHideConflicts()
        {
            return HasActiveProject && Project.ConflictCompilation != null && Project.ConflictCompilation.Conflicts.Count > 0;
        }

        private void CheckConflict(ConflictCriteria criteria)
        {
            Action action = () =>
            {
                if (Project != null && Project.Schedule != null)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    if (Project.AreConflictsShown)
                    {
                        OnShowHideConflicts();
                    }
                    Project.ConflictCompilation.CreateConflictCompilation(Project.Schedule, criteria);
                    Mouse.OverrideCursor = Cursors.Arrow;
                    if (!Project.AreConflictsShown)
                    {
                        OnShowHideConflicts();
                    }
                }
            };
            ExecuteAction("Поиск конфликтов", "Поиск конфликтов завершен", action);
        }

        private void OnCalcStatistic()
        {
            if (Project != null && Project.Schedule != null)
            {
                Action action = () =>
                {
                    Project.StatisticCompilation = new StatisticCompilation(Project.Schedule);
                };
                ExecuteAction("Вычисление статистики", "Статистика получена", action);
                OnOpenStatisticWindow();
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
            var fileName = Resx.ProjectFileDefaultName;
            var fileExt = Resx.ProjectFileExtensionNative;
            var filter = Resx.ProjectFileFilterNative;
            var dlg = new SaveFileDialog
            {
                FileName = fileName,
                DefaultExt = "." + fileExt,
                Filter = filter + "|*." + fileExt
            };
            var result = dlg.ShowDialog();
            if (result == true)
            {
                Action action = () => SaveLoadSchedule.Save(Project.Schedule, dlg.FileName);
                ExecuteAction("Сохранение расписания", "Расписание сохранено", action);
            }
        }

        private void OnOpenProject()
        {
            var fileName = Resx.ProjectFileDefaultName;
            var fileExt = Resx.ProjectFileExtensionNative;
            var filter = Resx.ProjectFileFilterNative;
            var dlg = new OpenFileDialog
            {
                FileName = fileName,
                DefaultExt = "." + fileExt,
                Filter = filter + "|*." + fileExt
            };
            var result = dlg.ShowDialog();
            if (result == true)
            {
                Action action = () =>
                {
                    var schedule = SaveLoadSchedule.Load(dlg.FileName);
                    SetNewProject(new ScheduleProject { Schedule = schedule });
                };
                ExecuteAction("Открытие расписания", "Расписание загружено", action);
            }
        }

        private void OnNewProject()
        {
            var schedule = new Schedule();
            schedule.InitStdTimeLine();
            schedule.AddYSG();
            SetNewProject(new ScheduleProject { Schedule = schedule });
        }

        private void OnLoadRandomSchedule()
        {
            Action action = () =>
            {
                _tableController.Tables.Clear();
                var schedule = new ScheduleRepository().Schedule;
                SetNewProject(new ScheduleProject { Schedule = schedule, ActiveYearOfStudy = schedule.YearsOfStudy.Any() ? schedule.YearsOfStudy.First() : null });
            };
            ExecuteAction("Генерация расписания", "Расписание сгенерировано", action);
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
            UpdateAll();
        }

        private bool CanExecuteHasActiveProject()
        {
            return HasActiveProject;
        }
        
        #endregion

        #region Update

        private void UpdateAll()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            _tableController.UpdateAll();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        public void AddClassRecord(ClassRecord classRecord)
        {
            
        }

        public void AddYearOfStudy()
        {
            UpdateStatus("Создание нового курса...");
            Mouse.OverrideCursor = Cursors.Wait;
            _tableController.AddYearOfStudy();
            Mouse.OverrideCursor = Cursors.Arrow;
            UpdateStatus("Новый курс создан");
        }

        public void AddSpecialization(YearOfStudy yearOfStudy)
        {
            UpdateStatus("Создание новой специальности...");
            Mouse.OverrideCursor = Cursors.Wait;
            _tableController.AddSpecialization(yearOfStudy);
            Mouse.OverrideCursor = Cursors.Arrow;
            UpdateStatus("Новая специальность создана");
        }

        public void AddGroup(Specialization spec)
        {
            UpdateStatus("Создание новой группы...");
            Mouse.OverrideCursor = Cursors.Wait;
            _tableController.AddGroup(Project.ActiveYearOfStudy, spec);
            Mouse.OverrideCursor = Cursors.Arrow;
            UpdateStatus("Новая группа создана");
        }

//        public void AddLecturer(object param)
//        {
//            if (AddLecturerDelegate != null)
//            {
//                AddLecturerDelegate(param as Lecturer);
//            }
//        }
//
//        public void AddSubject(object param)
//        {
//            if (AddSubjectDelegate != null)
//            {
//                AddSubjectDelegate(param as Subject);
//            }
//        }
//
//        public void RemoveClassRecord(object param)
//        {
//            if (RemoveClassRecordDelegate != null)
//            {
//                RemoveClassRecordDelegate(param as ClassRecord);
//            }
//        }
//
        public void RemoveYearOfStudy(YearOfStudy yearOfStudy)
        {
            UpdateStatus("Удаление курса...");
            Mouse.OverrideCursor = Cursors.Wait;
            _tableController.RemoveYearOfStudy(yearOfStudy);
            Mouse.OverrideCursor = Cursors.Arrow;
            UpdateStatus("Курс удален");
        }

        public void RemoveSpecialization(Specialization specialization)
        {
            UpdateStatus("Удаление специальности...");
            Mouse.OverrideCursor = Cursors.Wait;
            _tableController.RemoveSpecialization(specialization);
            Mouse.OverrideCursor = Cursors.Arrow;
            UpdateStatus("Специальность удалена");
        }

        public void RemoveGroup(Group @group)
        {
            UpdateStatus("Удаление группы...");
            Mouse.OverrideCursor = Cursors.Wait;
            _tableController.RemoveGroup(group);
            Mouse.OverrideCursor = Cursors.Arrow;
            UpdateStatus("Группа удалена");
        }

//        public void RemoveLecturer(object param)
//        {
//            if (RemoveLecturerDelegate != null)
//            {
//                RemoveLecturerDelegate(param as Lecturer);
//            }
//        }
//
//        public void RemoveSubject(object param)
//        {
//            if (RemoveSubjectDelegate != null)
//            {
//                RemoveSubjectDelegate(param as Subject);
//            }
//        }

        private void UpdateStatus(string status)
        {
            if (Project != null && Project.ProjectStatus != null)
            {
                //Project.ProjectStatus.Status = status;
            }
        }

        #endregion

        #region Helpers

        private void ExecuteAction(string executingStatus, string executedStatus, Action action)
        {
            UpdateStatus(executingStatus + "...");
            Mouse.OverrideCursor = Cursors.Wait;
            if (action != null)
            {
                action();
            }
            Mouse.OverrideCursor = Cursors.Arrow;
            UpdateStatus(executedStatus);
        }

        #endregion
    }
}
