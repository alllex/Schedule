using ScheduleData;

namespace Editor.ViewModels.Cards
{
    internal class GroupCardViewModel : HasProjectProperty
    {
        #region Properties

        #region Group

        private Group _group;
        public Group Group
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;
                    RaisePropertyChanged(() => Group);
                }
            }
        }

        #endregion

        #endregion


        #region Ctor

        public GroupCardViewModel(Group group)
        {
            Group = group;
        }

        #endregion
    }
}
