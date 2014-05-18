using System.Collections.ObjectModel;
using Editor.Helpers;

namespace Editor.Models
{
    class ScheduleProject : NotificationObject
    {

        #region CardClipboard

        private ObservableCollection<ClassRecord> _cardClipboard = new ObservableCollection<ClassRecord>();

        public ObservableCollection<ClassRecord> CardClipboard
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
