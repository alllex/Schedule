namespace Editor.ViewModels.Windows
{
    class ListsEditWindowViewModel : HasProjectProperty
    {

        #region Properties

        #region ActiveTab

        private TabCategory _activeTab;
        public TabCategory ActiveTab
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

        public ListsEditWindowViewModel(TabCategory initTab = TabCategory.Groups)
        {
            ActiveTab = initTab;
        }

        #endregion
    }
}
