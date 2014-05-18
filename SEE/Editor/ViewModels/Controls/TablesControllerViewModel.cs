using System.Collections.ObjectModel;

namespace Editor.ViewModels.Controls
{
    class TablesControllerViewModel : HasClassesScheduleProperty
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
