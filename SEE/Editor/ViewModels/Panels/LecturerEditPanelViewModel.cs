using System.Collections.ObjectModel;
using System.Linq;
using Editor.Models;

namespace Editor.ViewModels
{
    class LecturerEditPanelViewModel : BaseViewModel
    {

        #region Properties

        #endregion

        #region Ctor

        public LecturerEditPanelViewModel(ClassesSchedule classesSchedule)
        {
            ClassesSchedule = classesSchedule;
        }

        #endregion

        protected override void ClassesScheduleOnPropertyChanged()
        {
            
        }
    }
}
