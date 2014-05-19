using System.Collections.ObjectModel;
using Editor.Helpers;
using Editor.ViewModels;
using Editor.ViewModels.Cards;

namespace Editor.Models
{
    public class ScheduleProject : HasProjectProperty
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

        #region ClassesSchedule

        private ClassesSchedule _classesSchedule;

        public ClassesSchedule ClassesSchedule
        {
            get { return _classesSchedule; }
            set
            {
                if (_classesSchedule != value)
                {
                    _classesSchedule = value;
                    RaisePropertyChanged(() => ClassesSchedule);
                }
            }
        }

        #endregion

        
    }
}
