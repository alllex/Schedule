using Editor.Models;

namespace Editor.ViewModels.Windows
{
    enum ListsEditorTab
    {
        Groups,
        Lecturers,
        Classrooms,
        Specializations,
        YearsOfStudy
    }

    class ListsEditWindowViewModel : HasClassesScheduleProperty
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

        public ListsEditWindowViewModel(ListsEditorTab initTab = ListsEditorTab.Groups)
        {
            ActiveTab = initTab;
        }

        #endregion
    }
}
