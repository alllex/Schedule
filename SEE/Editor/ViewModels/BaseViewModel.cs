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
                    if (_classesSchedule != null && _classesSchedule.ItemChangedProperty != null)
                    {
                        _classesSchedule.ItemChangedProperty -= RaiseClassesScheduleChanged;
                    }
                    _classesSchedule = value;
                    if (_classesSchedule != null)
                    {
                        _classesSchedule.ItemChangedProperty += RaiseClassesScheduleChanged;
                    }
                    ClassesScheduleOnPropertyChanged();
                    RaisePropertyChanged(() => ClassesSchedule);
                }
            }
        }

        #endregion

        private void RaiseClassesScheduleChanged()
        {
            ClassesScheduleOnPropertyChanged();
        }

        protected abstract void ClassesScheduleOnPropertyChanged();
    }

}