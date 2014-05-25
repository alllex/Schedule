using System.Windows;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using ScheduleData;

namespace Editor.ViewModels.Panels.Edit
{
    class SubjectEditPanelViewModel : HasProjectProperty
    {
        #region Commands

        public ICommand AddSubjectCommand { get { return new DelegateCommand(OnAddSubject); } }
        public ICommand RemoveSubjectCommand { get { return new DelegateCommand(OnRemoveSubject); } }

        private void OnRemoveSubject()
        {
            MessageBox.Show("Remove");
            //var year = param as Subject;
            //if (year == null) return;
            //Project.Schedule.RemoveSubject(year);
        }

        #endregion

        #region Command Handlers

        private void OnAddSubject()
        {
            Project.Schedule.AddSubject(new Subject { Name = "Новый" });
        }

        #endregion
    }
}
