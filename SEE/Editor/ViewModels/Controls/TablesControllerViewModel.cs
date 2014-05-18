using System.Collections.ObjectModel;
using Editor.Models;

namespace Editor.ViewModels.Controls
{
    class TablesControllerViewModel : HasClassesScheduleProperty
    {

        protected override void ClassesScheduleOnPropertyChanged()
        {
            SetTables();
        }

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

        private readonly ScheduleProject _project;

        #endregion

        #region Ctor

        public TablesControllerViewModel(ScheduleProject project)
        {
            _project = project;
            ClassesSchedule = project.ClassesSchedule;
        }

        #endregion

        private void SetTables()
        {
            Tables = new ObservableCollection<TableViewModel>();
            foreach (var yearOfStudy in ClassesSchedule.YearsOfStudy)
            {
                Tables.Add(new TableViewModel(_project){ClassesSchedule = ClassesSchedule, YearOfStudy = yearOfStudy});
            }
        }
    }
}
