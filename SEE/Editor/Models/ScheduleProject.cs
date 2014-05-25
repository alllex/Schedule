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

        #region Schedule

        private Schedule _schedule;

        public Schedule Schedule
        {
            get { return _schedule; }
            set
            {
                if (_schedule != value)
                {
                    _schedule = value;
                    RaisePropertyChanged(() => Schedule);
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

        #region ScheduleController

        private ScheduleController _scheduleController;

        public ScheduleController ScheduleController
        {
            get { return _scheduleController; }
            set
            {
                if (_scheduleController != value)
                {
                    _scheduleController = value;
                    RaisePropertyChanged(() => ScheduleController);
                }
            }
        }

        #endregion

        #region ProjectStatus

        private ProjectStatus _projectStatus;

        public ProjectStatus ProjectStatus
        {
            get { return _projectStatus; }
            set
            {
                if (_projectStatus != value)
                {
                    _projectStatus = value;
                    RaisePropertyChanged(() => ProjectStatus);
                }
            }
        }

        #endregion

        

        
    }
}
