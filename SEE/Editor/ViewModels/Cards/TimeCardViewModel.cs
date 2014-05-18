using Editor.Models;

namespace Editor.ViewModels
{
    class TimeCardViewModel : HasClassesScheduleProperty
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

        public TimeCardViewModel(TimeInterval time)
        {
            Time = time.BeginTime + "-\n" + time.EndTime;
        }

        #endregion

        protected override void ClassesScheduleOnPropertyChanged()
        {
            
        }
    }
}
