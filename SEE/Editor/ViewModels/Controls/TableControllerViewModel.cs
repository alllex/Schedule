using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

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

        public void UpdateTables()
        {
            Tables.Clear();
            foreach (var yearOfStudy in Project.ClassesSchedule.YearsOfStudy)
            {
                Tables.Add(new TableViewModel{Project = Project, YearOfStudy = yearOfStudy});
            }
            if (Tables.Count > 0)
            {
                SelectedIndex = 0;
            }
        }
    }
}
