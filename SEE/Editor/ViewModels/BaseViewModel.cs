using Editor.Helpers;
using Editor.Models;

namespace Editor.ViewModels
{
    public abstract class BaseViewModel : NotificationObject
    {
        #region ClassesSchedule

        private ClassesSchedule _classesSchedule;

        public ClassesSchedule ClassesSchedule
        {
            get { return _classesSchedule; }
            set
            {
                if (_classesSchedule != value)
                {
                    _classesSchedule = value;
                    ClassesScheduleOnPropertyChanged();
                    RaisePropertyChanged(() => ClassesSchedule);
                }
            }
        }

        #endregion

        protected abstract void ClassesScheduleOnPropertyChanged();
    }
}