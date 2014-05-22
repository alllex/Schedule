using System.Collections.ObjectModel;
using Editor.ViewModels;
using Editor.ViewModels.Cards;
using ScheduleData;
using ScheduleData.DataMining;
using ScheduleData.SearchConflicts;

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

        #region ConflictCompilation

        private ConflictCompilation _conflictCompilation;

        public ConflictCompilation ConflictCompilation
        {
            get { return _conflictCompilation; }
            set
            {
                if (_conflictCompilation != value)
                {
                    _conflictCompilation = value;
                    RaisePropertyChanged(() => ConflictCompilation);
                }
            }
        }

        #endregion

        #region AreConflictsShown

        private bool _areConflictsShown;

        public bool AreConflictsShown
        {
            get { return _areConflictsShown; }
            set
            {
                if (_areConflictsShown != value)
                {
                    _areConflictsShown = value;
                    RaisePropertyChanged(() => AreConflictsShown);
                }
            }
        }

        #endregion

        #region ActiveYearOfStudy

        private YearOfStudy _activeYearOfStudy;

        public YearOfStudy ActiveYearOfStudy
        {
            get { return _activeYearOfStudy; }
            set
            {
                if (_activeYearOfStudy != value)
                {
                    _activeYearOfStudy = value;
                    RaisePropertyChanged(() => ActiveYearOfStudy);
                }
            }
        }

        #endregion

        
    }
}
