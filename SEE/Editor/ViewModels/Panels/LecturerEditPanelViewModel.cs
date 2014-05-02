using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleData;
using ScheduleData.Interfaces;

namespace Editor.ViewModels
{
    class LecturerEditPanelViewModel : BaseViewModel
    {

        #region Properties

        #region Degree

        public string Degree
        {
            get { return _lecturer.Degree; }
            set
            {
                if (_lecturer.Degree != value)
                {
                    _lecturer.Degree = value;
                    RaisePropertyChanged(() => Degree);
                }
            }
        }

        #endregion

        #region Name

        public string Name
        {
            get { return _lecturer.Name; }
            set
            {
                if (_lecturer.Name != value)
                {
                    _lecturer.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        #endregion

        #endregion

        #region Ctor

        private ILecturer _lecturer;
        public LecturerEditPanelViewModel(ILecturer lecturer)
        {
            _lecturer = lecturer;
        }

        #endregion
    }
}
