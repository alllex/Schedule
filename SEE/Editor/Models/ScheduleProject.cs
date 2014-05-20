using System.Collections.ObjectModel;
using Editor.ViewModels;
using Editor.ViewModels.Cards;

namespace Editor.Models
{
    public class ScheduleProject : BaseViewModel
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

        #region StatisticCompilation

        private StatisticCompilation _statisticCompilation;

        public StatisticCompilation StatisticCompilation
        {
            get { return _statisticCompilation; }
            set
            {
                if (_statisticCompilation != value)
                {
                    _statisticCompilation = value;
                    RaisePropertyChanged(() => StatisticCompilation);
                }
            }
        }

        #endregion
        
    }
}
