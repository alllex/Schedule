using Editor.Models;

namespace Editor.ViewModels.Cards
{
    class TitleCardViewModel : BaseViewModel
    {
        #region Properties

        #region Title

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged(() => Title);
                }
            }
        }

        #endregion

        #endregion

        #region Ctor

        public TitleCardViewModel(HavingName name)
        {
            Title = name.Name;
        }

        #endregion

        protected override void ClassesScheduleOnPropertyChanged()
        {
            
        }
    }
}
