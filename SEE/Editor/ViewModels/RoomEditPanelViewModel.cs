using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleData;

namespace Editor.ViewModels
{
    class RoomEditPanelViewModel : BaseViewModel
    {

        #region Properties

        #region Address

        public string Address
        {
            get { return _classroom.Address; }
            set
            {
                if (_classroom.Address != value)
                {
                    _classroom.Address = value;
                    RaisePropertyChanged(() => Address);
                }
            }
        }

        #endregion

        #region Name

        public string Name
        {
            get { return _classroom.Name; }
            set
            {
                if (_classroom.Name != value)
                {
                    _classroom.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        #endregion

        #endregion

        #region Ctor

        private IClassroom _classroom;
        public RoomEditPanelViewModel(IClassroom classroom)
        {
            _classroom = classroom;
        }

        #endregion
    }
}
