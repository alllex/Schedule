using Editor.Helpers;
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
                    _project = value;
                    RaisePropertyChanged(() => Project);
                }
            }
        }

        #endregion
    }
}
