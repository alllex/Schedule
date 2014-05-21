using System.Windows;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;

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
            //Project.ClassesSchedule.RemoveSubject(year);
        }

        #endregion

        #region Command Handlers

        private void OnAddSubject()
        {
            Project.ClassesSchedule.AddSubject(new Subject { Name = "Новый" });
        }

        #endregion
    }
}
