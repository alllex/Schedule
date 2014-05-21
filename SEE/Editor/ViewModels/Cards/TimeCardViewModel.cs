using System.Linq;
using Editor.Helpers;
using Editor.Models;
using ScheduleData;

namespace Editor.ViewModels.Cards
{
    class TimeCardViewModel : NotificationObject
    {
        #region Properties

        #region Time

        private string _time;
        public string Time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    RaisePropertyChanged(() => Time);
                }
            }
        }

        #endregion

        #endregion

        #region Ctor

        public TimeCardViewModel(ClassTime time)
        {
            var i = time.Number;
            Time = i > ClassTime.ClassIntervals.Count() || i < 0 ? "Whenever" : ClassTime.ClassIntervals[time.Number];
        }

        #endregion
    }
}
