using System;
using System.Windows.Input;
using Editor.Helpers;
using ScheduleData;

namespace Editor.ViewModels.Cards
{
    class SpecializationCardViewModel : HasProjectProperty
    {

        #region Properties

        #region Specialization

        private Specialization _specialization;
        public Specialization Specialization
        {
            get { return _specialization; }
            set
            {
                if (_specialization != value)
                {
                    _specialization = value;
                    RaisePropertyChanged(() => Specialization);
                }
            }
        }

        #endregion

        #endregion

        private readonly Action _updateViews;

        #region Ctor


        public SpecializationCardViewModel(Specialization specialization, Action updateViews)
        {
            Specialization = specialization;
            _updateViews = updateViews;
        }

        #endregion

        #region Commands

        public ICommand AddGroupCommand { get { return new DelegateCommand(OnAddGroup); } }

        private void OnAddGroup()
        {
            if (_updateViews != null)
            {
                var group = new Group { Name = "Новая группа", Specialization = Specialization, YearOfStudy = Project.ActiveYearOfStudy};
                Project.Schedule.AddGroup(group);
                _updateViews();
            }
        }

        #endregion
    }
}
