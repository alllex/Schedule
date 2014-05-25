using System;
using System.Windows.Input;
using Editor.Helpers;
using ScheduleData;

namespace Editor.ViewModels.Controls
{
    class LeftTopControlViewModel : HasProjectProperty
    {
        private readonly Action _updateViews;

        #region Ctor

        public LeftTopControlViewModel(Action updateViews)
        {
            _updateViews = updateViews;
        }

        #endregion

        #region Commands

        public ICommand AddSpecializationCommand { get { return new DelegateCommand(OnAddSpecialization); } }

        private void OnAddSpecialization()
        {
            if (_updateViews != null)
            {
                var spec = new Specialization {Name = "Новая специальность"};
                var group = new Group
                {
                    Name = "Новая группа",
                    Specialization = spec,
                    YearOfStudy = Project.ActiveYearOfStudy
                };
                Project.Schedule.AddSpecialization(spec);
                Project.Schedule.AddGroup(group);
                _updateViews();
            }
        }

        #endregion
    }
}
