using Editor.Models;

namespace Editor.ViewModels
{
    public abstract class HasProjectProperty : BaseViewModel
    {
        #region Project

        private ScheduleProject _project;

        public ScheduleProject Project
        {
            get { return _project; }
            set
            {
                if (_project != value)
                {
//                    if (_Project != null && _Project.ItemChangedProperty != null)
//                    {
//                        _Project.ItemChangedProperty -= RaiseProjectChanged;
//                    }
                    _project = value;
//                    if (_Project != null)
//                    {
//                        _Project.ItemChangedProperty += RaiseProjectChanged;
//                    }
                    //ProjectOnPropertyChanged();
                    RaisePropertyChanged(() => Project);
                }
            }
        }

        #endregion

//        private void RaiseProjectChanged()
//        {
//            ProjectOnPropertyChanged();
//        }
//
//        protected virtual void ProjectOnPropertyChanged(){}
    }
}
