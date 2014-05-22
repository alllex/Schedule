using Editor.Helpers;
using ScheduleData;

namespace Editor.ViewModels.Cards
{
    internal class SubtitleCardViewModel : HasProjectProperty
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

        public SubtitleCardViewModel(HavingName name)
        {
            _item = name;
        }

        #endregion
    }
}
