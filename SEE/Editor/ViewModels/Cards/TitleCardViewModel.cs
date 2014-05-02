using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleData;
using ScheduleData.Interfaces;

namespace Editor.ViewModels
{
    class TitleCardViewModel : BaseViewModel
    {
        #region Properties

        #region Title

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged(() => Title);
                }
            }
        }

        #endregion

        #endregion

        #region Ctor

        public TitleCardViewModel(IHavingName name)
        {
            Title = name.Name;
        }

        #endregion
    }
}
