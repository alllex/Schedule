using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using ScheduleData;

namespace Editor.ViewModels
{
    class TimeCardViewModel : BaseViewModel
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

        public TimeCardViewModel(ITimeInterval time)
        {
            Time = time.ToString();
        }

        #endregion

    }
}
