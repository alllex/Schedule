using System.Collections.ObjectModel;
using Editor.ViewModels.Cards;

namespace Editor.ViewModels.Controls
{
    class CardsPoolViewModel : BaseViewModel    
    {
        #region Cards

        private ObservableCollection<ClassCardViewModel> _cards = new ObservableCollection<ClassCardViewModel>();

        public ObservableCollection<ClassCardViewModel> Cards
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
