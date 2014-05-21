using System.Windows;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using ScheduleData;

namespace Editor.ViewModels.Panels.Edit
{
    class ClassroomEditPanelViewModel : HasProjectProperty
    {

        #region Commands

        public ICommand AddClassroomCommand { get { return new DelegateCommand(OnAddClassroom); } }
        public ICommand RemoveClassroomCommand { get { return new DelegateCommand(OnRemoveClassroom); } }

        private void OnRemoveClassroom()
        {
            MessageBox.Show("Remove");
            //var year = param as Classroom;
            //if (year == null) return;
            //Project.ClassesSchedule.RemoveClassroom(year);
        }

        #endregion

        #region Command Handlers

        private void OnAddClassroom()
        {
            Project.ClassesSchedule.AddClassroom(new Classroom { Name = "Новый" });
        }

        #endregion
    }
}
