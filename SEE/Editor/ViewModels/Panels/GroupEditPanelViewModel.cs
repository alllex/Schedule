using System.Collections.ObjectModel;
using System.Linq;
using Editor.Models;

namespace Editor.ViewModels
{
    class GroupEditPanelViewModel : BaseViewModel
    {

        #region Properties

        #endregion

        #region Ctor

        public GroupEditPanelViewModel(ClassesSchedule classesSchedule)
        {
            ClassesSchedule = classesSchedule;
        }

        #endregion

        protected override void ClassesScheduleOnPropertyChanged()
        {
            
        }
    }
}
