using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleData;
using ScheduleData.Interfaces;

namespace Editor.ViewModels
{
    internal class SubtitleCardViewModel : BaseViewModel
    {
        #region Properties

        #region Subtitle

        private string _subtitle;

        public string Subtitle
        {
            get { return _subtitle; }
            set
            {
                if (_subtitle != value)
                {
                    _subtitle = value;
                    RaisePropertyChanged(() => Subtitle);
                }
            }
        }

        #endregion

        #endregion

        #region Ctor

        public SubtitleCardViewModel(IHavingName name)
        {
            Subtitle = name.Name;
        }

        #endregion
    }
}
