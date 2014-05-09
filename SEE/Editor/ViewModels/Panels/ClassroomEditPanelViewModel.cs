using System.Collections.ObjectModel;
using System.Linq;
using Editor.Models;

namespace Editor.ViewModels
{
    class ClassroomEditPanelViewModel : BaseViewModel
    {

        #region Properties

        #endregion

        #region Ctor

        public ClassroomEditPanelViewModel(ClassesSchedule classesSchedule)
        {
            ClassesSchedule = classesSchedule;
        }

        #endregion

        protected override void ClassesScheduleOnPropertyChanged()
        {
            
        }
    }
}
