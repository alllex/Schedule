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
            get { return _room.Address; }
            set
            {
                if (_room.Address != value)
                {
                    _room.Address = value;
                    RaisePropertyChanged(() => Address);
                }
            }
        }

        #endregion

        #region Name

        public string Name
        {
            get { return _room.Name; }
            set
            {
                if (_room.Name != value)
                {
                    _room.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        #endregion

        #endregion

        #region Ctor

        private IRoom _room;
        public RoomEditPanelViewModel(IRoom room)
        {
            _room = room;
        }

        #endregion
    }
}
