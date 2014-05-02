using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleData;
using ScheduleData.Interfaces;

namespace Editor.ViewModels
{
    class GroupEditPanelViewModel : BaseViewModel
    {

        #region Properties

        #region Name

        public string Name
        {
            get { return _group.Name; }
            set
            {
                if (_group.Name != value)
                {
                    _group.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        #endregion

        #endregion

        #region Ctor

        private IGroup _group;
        public GroupEditPanelViewModel(IGroup group)
        {
            _group = group;
        }

        #endregion
    }
}
