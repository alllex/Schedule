using Editor.Helpers;
using ScheduleData;

namespace Editor.ViewModels.Cards
{
    class DayCardViewModel : NotificationObject
    {
        #region Properties

        #region Day

        private string _day;
        public string Day
        {
            get { return _day; }
            set
            {
                if (_day != value)
                {
                    _day = value;
                    RaisePropertyChanged(() => Day);
                }
            }
        }

        #endregion

        #endregion

        #region Ctor

        public DayCardViewModel(Weekdays day)
        {
            Day = day.ToString();
        }

        #endregion
    }
}
