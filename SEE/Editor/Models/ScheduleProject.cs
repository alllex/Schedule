using System.Collections.ObjectModel;
using Editor.Helpers;
using Editor.ViewModels;
using Editor.ViewModels.Cards;

namespace Editor.Models
{
    public class ScheduleProject : HasClassesScheduleProperty
    {

        #region CardClipboard

        private ObservableCollection<ClassCardViewModel> _cardClipboard = new ObservableCollection<ClassCardViewModel>();

        public ObservableCollection<ClassCardViewModel> CardClipboard
        {
            get { return _cardClipboard; }
            set
            {
                if (_cardClipboard != value)
                {
                    _cardClipboard = value;
                    RaisePropertyChanged(() => CardClipboard);
                }
            }
        }

        #endregion
        
    }
}
