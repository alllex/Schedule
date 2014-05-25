using System;
using System.Windows.Input;
using Editor.Helpers;
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

        private readonly Action _updateViews;

        #region Ctor

        public GroupCardViewModel(Group group, Action updateViews)
        {
            Group = group;
            _updateViews = updateViews;
        }

        #endregion

        #region Commands

        public ICommand RemoveGroupCommand { get { return new DelegateCommand(OnRemoveGroup);}}

        private void OnRemoveGroup()
        {
            if (_updateViews != null)
            {
                Project.Schedule.RemoveGroup(Group);
                _updateViews();
            }
        }

        #endregion
    }
}
