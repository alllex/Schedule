using Editor.Helpers;
using ScheduleData;

namespace Editor.ViewModels.Cards
{
    class TitleCardViewModel : NotificationObject
    {
        #region Properties

        #region Item

        private HavingName _item;
        public HavingName Item
        {
            get { return _item; }
            set
            {
                if (_item != value)
                {
                    _item = value;
                    RaisePropertyChanged(() => Item);
                }
            }
        }

        #endregion

        #endregion

        #region Ctor


        public TitleCardViewModel(HavingName name)
        {
            _item = name;
        }

        #endregion
    }
}
