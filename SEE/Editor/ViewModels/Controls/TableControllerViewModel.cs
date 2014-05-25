using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ScheduleData;

namespace Editor.ViewModels.Controls
{
    class TableControllerViewModel : HasProjectProperty
    {

        #region SelectedIndex

        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    RaisePropertyChanged(() => SelectedIndex);
                }
            }
        }

        #endregion

        #region Tables

        private ObservableCollection<TableViewModel> _tables = new ObservableCollection<TableViewModel>();

        public ObservableCollection<TableViewModel> Tables
        {
            get { return _tables; }
            set
            {
                if (_tables != value)
                {
                    _tables = value;
                    RaisePropertyChanged(() => Tables);
                }
            }
        }

        #endregion

        #region Fields

        public Action UpdateViews;
       
        #endregion

        #region Ctor

        public TableControllerViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var s = sender as TableControllerViewModel;
            if (s == null) return;
            if (e.PropertyName == "Project")
            {
                foreach (var tableViewModel in Tables)
                {
                    tableViewModel.YearOfStudy = null;
                    tableViewModel.Project = Project;
                }
            }
            else if (e.PropertyName == "SelectedIndex")
            {
                if (SelectedIndex >= 0)
                {
                    Project.ActiveYearOfStudy = Project.Schedule.YearsOfStudy[SelectedIndex];
                }
            }
        }

        #endregion

        public void ShowConflicts()
        {
            foreach (var tableViewModel in Tables)
            {
                tableViewModel.ShowConflicts();
            }
        }

        public void HideConflicts()
        {
            foreach (var tableViewModel in Tables)
            {
                tableViewModel.HideConflicts();
            }
        }

        public void UpdateAll()
        {
            var lastSelectedYearOfStudy = Tables.Count != 0 ? Tables[SelectedIndex].YearOfStudy : null;
            Tables.Clear();
            foreach (var yearOfStudy in Project.Schedule.YearsOfStudy)
            {
                if (Project.Schedule.HasGroups(yearOfStudy))
                {
                    Tables.Add(new TableViewModel(UpdateViews) { Project = Project, YearOfStudy = yearOfStudy });
                }
            }
            if (Tables.Count > 0)
            {
                if (lastSelectedYearOfStudy != null)
                {
                    var table = Tables.First(t => t.YearOfStudy == lastSelectedYearOfStudy);
                    SelectedIndex = Tables.IndexOf(table);
                }
                else
                {
                    SelectedIndex = 0;
                }
            }
        }

        public void AddYearOfStudy()
        {
            var group = Project.Schedule.AddYSG();
            Tables.Add(new TableViewModel(UpdateViews) { Project = Project, YearOfStudy = group.YearOfStudy });
        }

        public void RemoveYearOfStudy(YearOfStudy yearOfStudy)
        {
            Project.Schedule.RemoveYearOfStudy(yearOfStudy);
            Tables.Remove(Tables.First(t => t.YearOfStudy == yearOfStudy));
        }

        public void AddSpecialization(YearOfStudy yearOfStudy)
        {
            Project.Schedule.AddNewSG(yearOfStudy);
            UpdateAll();
        }

        public void RemoveSpecialization(Specialization specialization)
        {
            Project.Schedule.RemoveSpecialization(specialization);
            UpdateAll();
        }

        public void AddGroup(YearOfStudy year, Specialization spec)
        {
            var group = Project.Schedule.AddNewGroup(year, spec);
            var table = Tables.First(t => t.YearOfStudy == year);
            var index = Tables.IndexOf(table);
            Tables[index].AddGroup(group);
        }

        public void RemoveGroup(Group @group)
        {
            var lastSelectedYearOfStudy = Tables.Count != 0 ? Tables[SelectedIndex].YearOfStudy : null;
            Project.Schedule.RemoveGroup(group);
            var table = Tables.First(t => t.YearOfStudy == group.YearOfStudy);
            var index = Tables.IndexOf(table);
            Tables[index].RemoveGroup(group);
            if (Tables.Count > 0)
            {
                if (lastSelectedYearOfStudy != null)
                {
                    var table1 = Tables.First(t => t.YearOfStudy == lastSelectedYearOfStudy);
                    SelectedIndex = Tables.IndexOf(table1);
                }
                else
                {
                    SelectedIndex = 0;
                }
            }
        }
    }
}
