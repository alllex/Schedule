using Editor.Models;

namespace Editor.ViewModels
{
    enum ListsEditorTab
    {
        Groups,
        Lecturers,
        Classrooms,
        Specializations,
        YearsOfStudy
    }

    class ListsEditWindowViewModel : BaseViewModel
    {

        #region Properties

        #region ActiveTab

        private ListsEditorTab _activeTab;
        public ListsEditorTab ActiveTab
        {
            get { return _activeTab; }
            set
            {
                if (_activeTab != value)
                {
                    _activeTab = value;
                    RaisePropertyChanged(() => ActiveTab);
                }
            }
        }

        #endregion

        #endregion

        #region Ctor

        public ListsEditWindowViewModel(ClassesSchedule classesSchedule, ListsEditorTab initTab = ListsEditorTab.Groups)
        {
            ClassesSchedule = classesSchedule;
            ActiveTab = initTab;
        }

        #endregion

        protected override void ClassesScheduleOnPropertyChanged()
        {
            
        }
    }
}
