using Editor.Helpers;

namespace Editor.Models
{
    public class ProjectStatus : NotificationObject
    {

        #region Status

        private string _status;

        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged(() => Status);
                }
            }
        }

        #endregion

    }
}
