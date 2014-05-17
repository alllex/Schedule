using System.Collections.ObjectModel;
using Editor.UserControls;

namespace Editor.ViewModels.Controls
{
    class CardsPoolViewModel : BaseViewModel    
    {
        #region Cards

        private ObservableCollection<ClassCardViewMode> _cards = new ObservableCollection<ClassCardViewMode>();

        public ObservableCollection<ClassCardViewMode> Cards
        {
            get { return _cards; }
            set
            {
                if (_cards != value)
                {
                    _cards = value;
                    RaisePropertyChanged(() => Cards);
                }
            }
        }

        #endregion

        
    }
}
