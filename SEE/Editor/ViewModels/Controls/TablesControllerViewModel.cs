using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Editor.Models;
using Editor.UserControls;

namespace Editor.ViewModels.Controls
{
    class TablesControllerViewModel : BaseViewModel
    {

        protected override void ClassesScheduleOnPropertyChanged()
        {
            SetTables();
        }

        #region Tables

        private ObservableCollection<TableViewModel> _tables;

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

        public TablesControllerViewModel()
        {
            Debug.WriteLine(GetType() + " created");
        }

        private void SetTables()
        {
            Tables = new ObservableCollection<TableViewModel>();
            foreach (var yearOfStudy in ClassesSchedule.YearsOfStudy)
            {
                Tables.Add(new TableViewModel {ClassesSchedule = ClassesSchedule, YearOfStudy = yearOfStudy});
            }
        }
    }
}
