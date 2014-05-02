using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleData;
using ScheduleData.Interfaces;

namespace Editor.ViewModels
{
    class DayCardViewModel : BaseViewModel
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
