using System.Windows.Input;
using Editor.Helpers;

namespace Editor.ViewModels.Windows
{
    class StatisticWindowViewModel : HasProjectProperty
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

        #region Commands

        public ICommand CalcStatisticCommand { get; private set; }

        #endregion

        #region Ctor

        public StatisticWindowViewModel(ICommand calcStatisticCommand, TabCategory initTab = TabCategory.Groups)
        {
            ActiveTab = initTab;
            CalcStatisticCommand = calcStatisticCommand;
        }

        #endregion
    }
}
